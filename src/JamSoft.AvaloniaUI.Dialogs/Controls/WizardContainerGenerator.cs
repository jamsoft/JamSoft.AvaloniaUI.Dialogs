using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using Avalonia.Reactive;
using Avalonia.Threading;

namespace JamSoft.AvaloniaUI.Dialogs.Controls;

/// <summary>
/// The Wizard container generator
/// </summary>
public class WizardContainerGenerator 
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="owner"></param>
    public WizardContainerGenerator(Wizard owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// The owner wizard control
    /// </summary>
    public Wizard Owner;

    /// <inheritdoc/>
    public Control CreateContainer(object item)
    {
        var step = (WizardStep)base.CreateContainer(item, 0, null);

        step.Bind(WizardStep.ProgressPlacementProperty, new OwnerBinding<Dock>(
            step,
            Wizard.ButtonPlacementProperty));

        if (step.HeaderTemplate == null)
        {
            step.Bind(HeaderedContentControl.HeaderTemplateProperty, new OwnerBinding<IDataTemplate>(
                step,
                ItemsControl.ItemTemplateProperty!));
        }

        if (step.Header == null)
        {
            if (item is HeaderedContentControl headered)
            {
                step.Header = headered.Header;
            }
            else
            {
                if (!(step.DataContext is Control))
                {
                    step.Header = step.DataContext;
                }
            }
        }

        if (!(step.Content is Control))
        {
            step.Bind(ContentControl.ContentTemplateProperty, new OwnerBinding<IDataTemplate>(
                step,
                Wizard.ContentTemplateProperty!));
        }

        return step;
    }

    private class OwnerBinding<T> : AvSingleSubscriberObservableBase<T>
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
            //_ownerSubscription = ControlLocator.Track(_step, 0, typeof(Wizard)).Subscribe(OwnerChanged);
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
                // _propertySubscription = wizard.GetObservable(_ownerProperty)
                //     .Subscribe(x => PublishNext(x));
            }
        }
    }

    private abstract class AvSingleSubscriberObservableBase<T> : IObservable<T>, IDisposable
    {
        private Exception? _error;
        private IObserver<T>? _observer;
        private bool _completed;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _ = observer ?? throw new ArgumentNullException(nameof(observer));
            Dispatcher.UIThread.VerifyAccess();

            if (_observer != null)
            {
                throw new InvalidOperationException("The observable can only be subscribed once.");
            }

            if (_error != null)
            {
                observer.OnError(_error);
            }
            else if (_completed)
            {
                observer.OnCompleted();
            }
            else
            {
                _observer = observer;
                Subscribed();
            }

            return this;
        }

        public virtual void Dispose()
        {
            Unsubscribed();
            _observer = null;
        }

        protected abstract void Unsubscribed();

        protected void PublishNext(T value)
        {
            _observer?.OnNext(value);
        }

        protected void PublishCompleted()
        {
            _completed = true;

            if (_observer != null)
            {
                _observer.OnCompleted();
                Unsubscribed();
                _observer = null;
            }
        }

        protected void PublishError(Exception error)
        {
            _error = error;

            if (_observer != null)
            {
                _observer.OnError(error);
                Unsubscribed();
                _observer = null;
            }
        }

        protected abstract void Subscribed();
    }
}