using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace JamSoft.AvaloniaUI.Dialogs.Controls;

/// <summary>
/// Defines a step in a Wizard control
/// </summary>
[PseudoClasses(":pressed", ":selected", ":complete")]
public class WizardStep : HeaderedContentControl, ISelectable
{
	/// <summary>
	/// Defines the <see cref="ProgressPlacement"/> property.
	/// </summary> 
	public static readonly StyledProperty<Dock> ProgressPlacementProperty =
		Wizard.ProgressPlacementProperty.AddOwner<WizardStep>();
	
	/// <summary>
	/// Defines the <see cref="IsSelected"/> property.
	/// </summary>
	public static readonly StyledProperty<bool> IsSelectedProperty =
		ListBoxItem.IsSelectedProperty.AddOwner<WizardStep>();
	
	/// <summary>
	/// Defines the <see cref="StepComplete" /> property
	/// </summary>
	public static readonly StyledProperty<bool> StepCompleteProperty = AvaloniaProperty.Register<WizardStep, bool>(
		nameof(StepComplete), defaultBindingMode: BindingMode.OneWay);
	
	/// <summary>
	/// The step complete property
	/// </summary>
	public bool StepComplete
	{
		get { return GetValue(StepCompleteProperty); }
		set
		{
			SetValue(StepCompleteProperty, value);
		}
	}
	
	static WizardStep()
	{
		SelectableMixin.Attach<WizardStep>(IsSelectedProperty);
		FocusableProperty.OverrideDefaultValue(typeof(WizardStep), true);
		DataContextProperty.Changed.AddClassHandler<WizardStep>((x, e) => x.UpdateHeader(e));
		StepCompleteProperty.Changed.AddClassHandler<WizardStep>((x, _) => x.SetStepComplete());
	}

	private void SetStepComplete()
	{
		PseudoClasses.Set(":complete", StepComplete);
	}

	/// <summary>
	/// Gets the tab strip placement.
	/// </summary>
	/// <value>
	/// The tab strip placement.
	/// </value>
	public Dock ProgressPlacement
	{
		get { return GetValue(ProgressPlacementProperty); }
	}
	
	/// <summary>
	/// Gets or sets the selection state of the item.
	/// </summary>
	public bool IsSelected
	{
		get { return GetValue(IsSelectedProperty); }
		set { SetValue(IsSelectedProperty, value); }
	}
	
	private void UpdateHeader(AvaloniaPropertyChangedEventArgs obj)
	{
		if (Header == null)
		{
			if (obj.NewValue is IHeadered headered)
			{
				if (Header != headered.Header)
				{
					Header = headered.Header;
				}
			}
			else
			{
				if (!(obj.NewValue is IControl))
				{
					Header = obj.NewValue;
				}
			}
		}
		else
		{
			if (Header == obj.OldValue)
			{
				Header = obj.NewValue;
			}
		}          
	}
}