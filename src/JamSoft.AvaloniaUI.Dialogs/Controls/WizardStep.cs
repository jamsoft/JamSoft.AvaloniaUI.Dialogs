using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;

namespace JamSoft.AvaloniaUI.Dialogs.Controls;

[PseudoClasses(":pressed", ":selected")]
public class WizardStep : HeaderedContentControl, ISelectable
{
	public Type StyleKey
	{
		get
		{
			return typeof(WizardStep);
		}
	}
	
	/// <summary>
	/// Defines the <see cref="TabStripPlacement"/> property.
	/// </summary> 
	public static readonly StyledProperty<Dock> TabStripPlacementProperty =
		Wizard.TabStripPlacementProperty.AddOwner<WizardStep>();
	
	/// <summary>
	/// Defines the <see cref="IsSelected"/> property.
	/// </summary>
	public static readonly StyledProperty<bool> IsSelectedProperty =
		ListBoxItem.IsSelectedProperty.AddOwner<WizardStep>();
	
	static WizardStep()
	{
		SelectableMixin.Attach<WizardStep>(IsSelectedProperty);
		//PressedMixin.Attach<WizardStep>();
		FocusableProperty.OverrideDefaultValue(typeof(WizardStep), true);
		DataContextProperty.Changed.AddClassHandler<WizardStep>((x, e) => x.UpdateHeader(e));
	}
	
	/// <summary>
	/// Gets the tab strip placement.
	/// </summary>
	/// <value>
	/// The tab strip placement.
	/// </value>
	public Dock TabStripPlacement
	{
		get { return GetValue(TabStripPlacementProperty); }
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