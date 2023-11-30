using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Diagnostics;

namespace maui_repro.Extensions;

public class OnScreenSizeSource : INotifyPropertyChanged
{
    private object _defaultValue;
    private object _extraSmallValue;
    private object _smallValue;
    private object _mediumValue;
    private object _largeValue;
    private object _extraLargeValue;

    public object DefaultValue
    {
        get => _defaultValue;
        set
        {
            _defaultValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public object ExtraSmallValue
    {
        get => _extraSmallValue;
        set
        {
            _extraSmallValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public object SmallValue
    {
        get => _smallValue;
        set
        {
            _smallValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public object MediumValue
    {
        get => _mediumValue;
        set
        {
            _mediumValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public object LargeValue
    {
        get => _largeValue;
        set
        {
            _largeValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public object ExtraLargeValue
    {
        get => _extraLargeValue;
        set
        {
            _extraLargeValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    private object _currentValue;

    public object Value
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public OnScreenSizeSource()
    {
        WeakReferenceMessenger.Default.Register<OnScreenSizeSource, OnScreenSizeExtension.ScreenSizeChangedMessage>(this, OnScreenSizeChanged);
    }

    private void OnScreenSizeChanged(OnScreenSizeSource r, OnScreenSizeExtension.ScreenSizeChangedMessage m)
    {
        var newValue = m.NewValue;

        Value = newValue switch
        {
            var width when width < 50 => ExtraSmallValue ?? DefaultValue,   // Extra Small
            var width when width < 599 => SmallValue ?? DefaultValue,   // Small
            var width when width < 839 => MediumValue ?? DefaultValue,   // Medium
            var width when width < 1600 => LargeValue ?? DefaultValue,  // Large
            var width when width >= 1600 => ExtraLargeValue ?? DefaultValue,  // Extra Large
            _ => DefaultValue  // Default for undefined cases
        };

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));

    }
    public event PropertyChangedEventHandler PropertyChanged;
}
