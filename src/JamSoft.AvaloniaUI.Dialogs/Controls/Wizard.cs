using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Metadata;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Controls
{
    /// <summary>
    /// A wizard control that displays a series of elements in a workflow style progression.
    /// </summary>
    [TemplatePart("PART_StepsPresenter", typeof(ItemsControl))]
    [TemplatePart("PART_ButtonsPresenter", typeof(DockPanel))]
    public class Wizard : TemplatedControl
    {
        internal ContentPresenter? ContentPart;
        
        internal DockPanel? ButtonsPresenterPart { get; private set; }
        
        private Button? CompleteButton { get; set; }

        private Button? PreviousButton { get; set; }

        private Button? NextButton { get; set; }
        
        private ICommand MoveNextCommand => new DelegateCommand(MoveNextCommandExecuted);
        private ICommand MovePreviousCommand => new DelegateCommand(MovePreviousCommandExecuted);
        
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public WizardStep? SelectedItem { get; set; }

        /// <summary>
        /// The index of the active WizardStep
        /// </summary>
        public int SelectedIndex { get; set; } = 0;

        /// <summary>
        /// Defines the <see cref="SelectedIndex"/> property.
        /// </summary>
        public static readonly DirectProperty<Wizard, int> SelectedIndexProperty =
            AvaloniaProperty.RegisterDirect<Wizard, int>(
                nameof(SelectedIndex),
                o => o.SelectedIndex,
                (o, v) => o.SelectedIndex = v,
                unsetValue: 0,
                defaultBindingMode: BindingMode.TwoWay);
        
        /// <summary>
        /// Defines the <see cref="SelectedItem"/> property.
        /// </summary>
        public static readonly DirectProperty<Wizard, WizardStep?> SelectedItemProperty =
            AvaloniaProperty.RegisterDirect<Wizard, WizardStep?>(
                nameof(SelectedItem),
                o => o.SelectedItem,
                (o, v) => o.SelectedItem = v,
                defaultBindingMode: BindingMode.OneWay, enableDataValidation: true);
        
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
        public static readonly StyledProperty<IDataTemplate?> ContentTemplateProperty =
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
        /// Defines the <see cref="Steps"/> property.
        /// </summary>
        public static readonly StyledProperty<List<WizardStep>?> StepsProperty =
            AvaloniaProperty.Register<Wizard, List<WizardStep>?>(nameof(Steps));
        
        /// <summary>
        /// Gets or sets a collection used to generate the content of the <see cref="Wizard"/>.
        /// </summary>
        [Content]
        public List<WizardStep>? Steps
        {
            get => GetValue(StepsProperty);
            set => SetValue(StepsProperty, value);
        }
        
        /// <summary>
        /// Initializes static members of the <see cref="Wizard"/> class.
        /// </summary>
        public Wizard()
        {
            AffectsMeasure<Wizard>(ButtonPlacementProperty);
            DataContextProperty.Changed.AddClassHandler<Wizard>((x, _) => x.HandleDataContextChanged());
            Steps = new List<WizardStep>();
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
            get { return GetValue(ContentTemplateProperty)!; }
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

        private void StepCompleted(AvaloniaPropertyChangedEventArgs args)
        {
            if (args.NewValue is bool && (bool)args.NewValue)
            {
                NextButton!.IsEnabled = true;
            }
            else
            {
                NextButton!.IsEnabled = false;
            }
            
            var list = Steps;
            if (args.Sender.Equals(list.LastOrDefault()) && (args.NewValue is bool && (bool)args.NewValue))
            {
                CompleteButton!.IsEnabled = true;
            }
            else
            {
                CompleteButton!.IsEnabled = false;
            }
        }
        
        private void UpdateSelectedContent()
        {
            if (ContentPart != null)
            {
                SelectedItem = Steps?[SelectedIndex];
                ContentPart.SetValue(ContentControl.ContentProperty, SelectedItem?.Content);
            }
        }

        private void HandleDataContextChanged()
        {
            WizardStep.StepCompleteProperty.Changed.AddClassHandler<WizardStep>((x, args) => StepCompleted(args));
        }
        
        /// <inheritdoc/>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            //ItemsPresenterPart = e.NameScope.Get<ItemsControl>("PART_StepsPresenter");
            ButtonsPresenterPart = e.NameScope.Get<DockPanel>("PART_ButtonsPresenter");
            ContentPart = e.NameScope.Get<ContentPresenter>("PART_SelectedContentHost");

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
                Content = CompleteButtonContent,
                Command = (DataContext as IDialogViewModel)!.AcceptCommand
            };
            ButtonsPresenterPart.Children.Add(CompleteButton);
            DockPanel.SetDock(CompleteButton, Dock.Right);

            CompleteButton.IsEnabled = false;
            CompleteButton.IsVisible = false;
            NextButton.IsEnabled = false;
            PreviousButton.IsEnabled = false;
            
            UpdateSelectedContent();
        }

        private void MoveNextCommandExecuted()
        {
            if (SelectedIndex == Steps?.Count - 1)
            {
                return;
            }
            
            SelectedIndex++;
            UpdateSelectedContent();
            SetButtons();
        }
        
        private void MovePreviousCommandExecuted()
        {
            if (SelectedIndex == 0)
            {
                return;
            }
            
            SelectedIndex--;
            UpdateSelectedContent();
            SetButtons();
        }
        
        private void SetButtons()
        {
            NextButton!.IsEnabled = false;
            PreviousButton!.IsEnabled = true;

            var list = Steps;
            var selected = SelectedItem;

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
