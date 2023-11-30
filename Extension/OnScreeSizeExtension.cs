using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Diagnostics;

namespace maui_repro.Extensions;

[ContentProperty(nameof(Default))]
public class OnScreenSizeExtension : BindableObject, IMarkupExtension<BindingBase>
{

    public static readonly BindableProperty PageWidthProperty =
      BindableProperty.CreateAttached(nameof(PageWidth), typeof(double), typeof(OnScreenSizeExtension), default(double), propertyChanged: OnPageWidthChanged);

    public double PageWidth
    {
        get { return (double)GetValue(PageWidthProperty); }
        set { SetValue(PageWidthProperty, value); }
    }

    private static void OnPageWidthChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Debug.WriteLine(newValue);
        WeakReferenceMessenger.Default.Send(new ScreenSizeChangedMessage((double)newValue));
    }

    public Type TypeConverter { get; set; }
    public object Default { get; set; }
    public object ExtraSmall { get; set; }
    public object Small { get; set; }
    public object Medium { get; set; }
    public object Large { get; set; }
    public object ExtraLarge { get; set; }


    static OnScreenSizeExtension() { }

    /// <summary>
    /// Provides a binding based on the screen size using the (optional) specified <see cref="TypeConverter"/>.
    /// </summary>
    /// <param name="serviceProvider">The service provider for this markup extension.</param>
    /// <returns>A binding based on the screen size.</returns>
    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        var typeConverter = TypeConverter != null ? (TypeConverter)Activator.CreateInstance(TypeConverter) : null;

        var widthSource = new OnScreenSizeSource
        {
            DefaultValue = typeConverter?.ConvertFromInvariantString((string)Default) ?? Default,
            ExtraSmallValue = GetValueOrPrevious(typeConverter, ExtraSmall),
            SmallValue = GetValueOrPrevious(typeConverter, Small, ExtraSmall),
            MediumValue = GetValueOrPrevious(typeConverter, Medium, Small, ExtraSmall),
            LargeValue = GetValueOrPrevious(typeConverter, Large, Medium, Small, ExtraSmall),
            ExtraLargeValue = GetValueOrPrevious(typeConverter, ExtraLarge, Large, Medium, Small, ExtraSmall)
        };



        return new Binding
        {
            Mode = BindingMode.OneWay,
            Path = nameof(OnScreenSizeSource.Value),
            Source = widthSource
        };
    }

    /// <summary>
    /// Gets the non-null value from a set of values, applying the (optional) specified <see cref="TypeConverter"/>.
    /// </summary>
    /// <param name="typeConverter">The type converter to use for conversion.</param>
    /// <param name="values">The values to choose from.</param>
    /// <returns>The selected value or the default value with type conversion applied.</returns>
    private object GetValueOrPrevious(TypeConverter typeConverter, params object[] values)
    {
        var nonNullValue = values.FirstOrDefault(v => v != null);

        return typeConverter?.ConvertFromInvariantString(nonNullValue?.ToString()) ?? nonNullValue ?? (typeConverter?.ConvertFromInvariantString((string)Default) ?? Default);
    }


    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);

    public class ScreenSizeChangedMessage
    {
        public double NewValue { get; }

        public ScreenSizeChangedMessage(double newValue)
        {
            NewValue = newValue;
        }
    }
}
