using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using Avalonia.Reactive;

namespace JamSoft.AvaloniaUI.Dialogs.Controls;

/// <summary>
/// The Wizard container generator
/// </summary>
public class WizardContainerGenerator : ItemContainerGenerator<WizardStep>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="owner"></param>
    public WizardContainerGenerator(Wizard owner)
        : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
    {
        Owner = owner;
    }

    /// <summary>
    /// The owner wizard control
    /// </summary>
    public new Wizard Owner;

    /// <inheritdoc/>
    protected override IControl CreateContainer(object item)
    {
        var step = (WizardStep)base.CreateContainer(item);

        step.Bind(WizardStep.ProgressPlacementProperty, new OwnerBinding<Dock>(
            step,
            Wizard.ButtonPlacementProperty));

        if (step.HeaderTemplate == null)
        {
            step.Bind(HeaderedContentControl.HeaderTemplateProperty, new OwnerBinding<IDataTemplate>(
                step,
                ItemsControl.ItemTemplateProperty));
        }

        if (step.Header == null)
        {
            if (item is IHeadered headered)
            {
                step.Header = headered.Header;
            }
            else
            {
                if (!(step.DataContext is IControl))
                {
                    step.Header = step.DataContext;
                }
            }
        }

        if (!(step.Content is IControl))
        {
            step.Bind(ContentControl.ContentTemplateProperty, new OwnerBinding<IDataTemplate>(
                step,
                Wizard.ContentTemplateProperty));
        }

        return step;
    }

    private class OwnerBinding<T> : SingleSubscriberObservableBase<T>
    {
        private readonly WizardStep _step;
        private readonly StyledProperty<T> _ownerProperty;
        private IDisposable? _ownerSubscription;
        private IDisposable? _propertySubscription;

        public OwnerBinding(WizardStep step, StyledProperty<T> ownerProperty)
        {
            _step = step;
            _ownerProperty = ownerProperty;
        }

        protected override void Subscribed()
        {
            _ownerSubscription = ControlLocator.Track(_step, 0, typeof(Wizard)).Subscribe(OwnerChanged);
        }

        protected override void Unsubscribed()
        {
            _ownerSubscription?.Dispose();
            _ownerSubscription = null;
        }

        private void OwnerChanged(ILogical c)
        {
            _propertySubscription?.Dispose();
            _propertySubscription = null;

            if (c is Wizard wizard)
            {
                _propertySubscription = wizard.GetObservable(_ownerProperty)
                    .Subscribe(x => PublishNext(x));
            }
        }
    }
}