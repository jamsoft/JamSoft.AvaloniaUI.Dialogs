using System.Windows.Input;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Controls
{
    /// <summary>
    /// A wizard control that displays a series of elements in a workflow style progression.
    /// </summary>
    [TemplatePart("PART_StepsPresenter", typeof(ItemsPresenter))]
    [TemplatePart("PART_ButtonsPresenter", typeof(DockPanel))]
    public class Wizard : SelectingItemsControl, IContentPresenterHost
    {
        private ICommand MoveNextCommand => new DelegateCommand(MoveNextCommandExecuted);
        private ICommand MovePreviousCommand => new DelegateCommand(MovePreviousCommandExecuted);

        /// <summary>
        /// The default value for the <see cref="ItemsControl.ItemsPanel"/> property.
        /// </summary>
        private static readonly FuncTemplate<IPanel> DefaultPanel = new(() => new WrapPanel());
        
        /// <summary>
        /// Defines the <see cref="ButtonPlacement"/> property.
        /// </summary>
        public static readonly StyledProperty<Dock> ButtonPlacementProperty =
            AvaloniaProperty.Register<Wizard, Dock>(nameof(ButtonPlacement), defaultValue: Dock.Bottom);

        /// <summary>
        /// Defines the <see cref="ProgressPlacement"/> property.
        /// </summary>
        public static readonly StyledProperty<Dock> ProgressPlacementProperty =
            AvaloniaProperty.Register<Wizard, Dock>(nameof(ProgressPlacement), defaultValue: Dock.Top);
        
        /// <summary>
        /// Defines the <see cref="ContentTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ContentTemplateProperty =
            ContentControl.ContentTemplateProperty.AddOwner<Wizard>();

        /// <summary>
        /// The selected content property
        /// </summary>
        public static readonly StyledProperty<object?> SelectedContentProperty =
            AvaloniaProperty.Register<Wizard, object?>(nameof(SelectedContent));

        /// <summary>
        /// The selected content template property
        /// </summary>
        public static readonly StyledProperty<IDataTemplate?> SelectedContentTemplateProperty =
            AvaloniaProperty.Register<Wizard, IDataTemplate?>(nameof(SelectedContentTemplate));
        
        /// <summary>
        /// Defines the <see cref="NextButtonContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> NextButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(NextButtonContent), "Next");
        
        /// <summary>
        /// Defines the <see cref="PreviousButtonContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> PreviousButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(PreviousButtonContent), "Back");
        
        /// <summary>
        /// Defines the <see cref="CompleteButtonContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> CompleteButtonContentProperty =
            AvaloniaProperty.Register<Wizard, object>(nameof(CompleteButtonContent), "Complete");

        /// <summary>
        /// Initializes static members of the <see cref="Wizard"/> class.
        /// </summary>
        static Wizard()
        {
            SelectionModeProperty.OverrideDefaultValue<Wizard>(SelectionMode.AlwaysSelected);
            ItemsPanelProperty.OverrideDefaultValue<Wizard>(DefaultPanel);
            AffectsMeasure<Wizard>(ButtonPlacementProperty);
            SelectedItemProperty.Changed.AddClassHandler<Wizard>((x, _) => x.UpdateSelectedContent());
            DataContextProperty.Changed.AddClassHandler<Wizard>((x, _) => x.HandleDataContextChanged());
        }

        /// <summary>
        /// Gets or sets the button placement of the Wizard.
        /// </summary>
        public Dock ButtonPlacement
        {
            get { return GetValue(ButtonPlacementProperty); }
            set { SetValue(ButtonPlacementProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the progress placement of the Wizard.
        /// </summary>
        public Dock ProgressPlacement
        {
            get { return GetValue(ProgressPlacementProperty); }
            set { SetValue(ProgressPlacementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the default data template used to display the content of the selected WizardStep.
        /// </summary>
        public IDataTemplate ContentTemplate
        {
            get { return GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content of the selected WizardStep.
        /// </summary>
        /// <value>
        /// The content of the selected WizardStep.
        /// </value>
        public object? SelectedContent
        {
            get { return GetValue(SelectedContentProperty); }
            internal set { SetValue(SelectedContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content template for the selected WizardStep.
        /// </summary>
        /// <value>
        /// The content template of the selected WizardStep.
        /// </value>
        public IDataTemplate? SelectedContentTemplate
        {
            get { return GetValue(SelectedContentTemplateProperty); }
            internal set { SetValue(SelectedContentTemplateProperty, value); }
        }
        
        /// <summary>
        /// Next button content
        /// </summary>
        public object NextButtonContent
        {
            get { return GetValue(NextButtonContentProperty); }
            set { SetValue(NextButtonContentProperty, value); }
        }
        
        /// <summary>
        /// Previous button content
        /// </summary>
        public object PreviousButtonContent
        {
            get { return GetValue(PreviousButtonContentProperty); }
            set { SetValue(PreviousButtonContentProperty, value); }
        }

        /// <summary>
        /// Complete button content
        /// </summary>
        public object CompleteButtonContent
        {
            get { return GetValue(CompleteButtonContentProperty); }
            set { SetValue(CompleteButtonContentProperty, value); }
        }

        internal ItemsPresenter? ItemsPresenterPart;

        internal IContentPresenter? ContentPart;
        
        internal DockPanel? ButtonsPresenterPart { get; private set; }

        /// <inheritdoc/>
        IAvaloniaList<ILogical> IContentPresenterHost.LogicalChildren => LogicalChildren;

        /// <inheritdoc/>
        bool IContentPresenterHost.RegisterContentPresenter(IContentPresenter presenter)
        {
            return RegisterContentPresenter(presenter);
        }

        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {
            base.OnContainersMaterialized(e);
            UpdateSelectedContent();
        }

        protected override void OnContainersRecycled(ItemContainerEventArgs e)
        {
            base.OnContainersRecycled(e);
            UpdateSelectedContent();
        }

        private void UpdateSelectedContent()
        {
            if (SelectedIndex == -1)
            {
                SelectedContent = SelectedContentTemplate = null;
            }
            else
            {
                var container = SelectedItem as IContentControl ??
                    ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as IContentControl;
                SelectedContentTemplate = container?.ContentTemplate;
                SelectedContent = container?.Content;
            }
        }

        /// <summary>
        /// Called when an <see cref="IContentPresenter"/> is registered with the control.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        protected virtual bool RegisterContentPresenter(IContentPresenter presenter)
        {
            if (presenter.Name == "PART_SelectedContentHost")
            {
                ContentPart = presenter;
                return true;
            }

            return false;
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new WizardContainerGenerator(this);
        }

        private void HandleDataContextChanged()
        {
            WizardStep.StepCompleteProperty.Changed.Subscribe(x =>
            {
                if (x.Sender.Equals(SelectedItem) && x.NewValue.Value)
                {
                    NextButton!.IsEnabled = true;
                }
                else
                {
                    NextButton!.IsEnabled = false;
                }
                
                var list = (AvaloniaList<object>)Items;
                if (x.Sender.Equals(list.LastOrDefault()) && x.NewValue.Value)
                {
                    CompleteButton!.IsEnabled = true;
                }
                else
                {
                    CompleteButton!.IsEnabled = false;
                }
            });
        }
        
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            ItemsPresenterPart = e.NameScope.Get<ItemsPresenter>("PART_StepsPresenter");
            ButtonsPresenterPart = e.NameScope.Get<DockPanel>("PART_ButtonsPresenter");

            PreviousButton = new Button
            {
                Content = PreviousButtonContent,
                Command = MovePreviousCommand
            };
            ButtonsPresenterPart.Children.Add(PreviousButton);
            DockPanel.SetDock(PreviousButton, Dock.Left);
            
            NextButton = new Button
            {
                Content = NextButtonContent,
                Command = MoveNextCommand
            };
            ButtonsPresenterPart.Children.Add(NextButton);
            DockPanel.SetDock(NextButton, Dock.Right);

            CompleteButton = new Button
            {
                Content = "Complete",
                Command = (DataContext as IDialogViewModel)!.AcceptCommand
            };
            ButtonsPresenterPart.Children.Add(CompleteButton);
            DockPanel.SetDock(CompleteButton, Dock.Right);

            CompleteButton.IsEnabled = false;
            CompleteButton.IsVisible = false;
            NextButton.IsEnabled = false;
            PreviousButton.IsEnabled = false;
        }

        public Button? CompleteButton { get; set; }

        public Button? PreviousButton { get; set; }

        public Button? NextButton { get; set; }

        private void MoveNextCommandExecuted()
        {
            SelectedIndex++;
            SetButtons();
        }
        
        private void MovePreviousCommandExecuted()
        {
            SelectedIndex--;
            SetButtons();
        }
        
        private void SetButtons()
        {
            NextButton!.IsEnabled = false;
            PreviousButton!.IsEnabled = true;

            var list = (AvaloniaList<object>)Items;
            var selected = SelectedItem as WizardStep;

            if (selected!.Equals(list.LastOrDefault()))
            {
                NextButton!.IsVisible = false;
                CompleteButton!.IsVisible = true;
            }
            else
            {
                NextButton!.IsVisible = true;
                CompleteButton!.IsVisible = false;
            }

            NextButton.IsEnabled = CompleteButton.IsEnabled = selected.StepComplete;
        }
    }
}
