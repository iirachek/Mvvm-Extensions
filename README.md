# Mvvm-Extensions
The `Mvvm-Extensions` library provides base implementations of classes, required for WPF application which intends to utilize MVVM pattern. Additionaly, the library provides a few solutions to common problems that arise during GUI development.

# Features
- feature 1
- feature 2
- feature 3
- feature 4

## INotifyPropertyChanged base implementation 
This library provides a few mechanisms that allow to greatly simplify the initial development of WPF application that utilizes MVVM pattern. 

`PropertyChangedImplementation` is a class that provides base implementation of the essential `INotifyPropertyChanged` interface. 

Classes, that inherit `PropertyChangedImplementation` can invoke `PropertyChanged` event by calling `NotifyPropertyChanged()` method in their property setters. Name of the invoking property is pulled automatically.

##### Example
```
	class RectangleViewModel : PropertyChangedImplementation
    {
		int width = 0;
		int height = 0;
		
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    NotifyPropertyChanged();
                }
            }
        }
	}
```


## [DependsOn] attribute
In addition, the library contains two ways to simplify usage of properties, that are dependant upon other properties in their `get` accessor. 

Lets assume we have three properties in datacontext, all of which are displayed in the view: `width`, `height` and `area`, where `width` and `height` are standalone properties and `area` is a read-only property that returns a product of `width` and `height` properties. 

In a normal scenario changing the values of `width` or `height` properties will not change the displayed value of `area` property, unless `PropertyChanged` is explicitly invoked for it.

Properties, marked with `DependsOn` attribute automatically invoke `PropertyChanged` event when this event is invoked for one of the properties, specified in attribute parameters.

`DependsOn` attribute should be used in classes that implement INotifyPropertyChanged and either utilize `DependencyWatcher` class or inherit `PropertyChangedImplementation` class. 

This attribute allows to automatically invoke `PropertyChanged` event for properties that lack `set` accessor or their value depends on other properties.

[Sample](https://github.com/iirachek/Mvvm-Extensions/blob/master/src/MvvmExtensions.Samples/ExampleViews/PropertyChangedImplementationExample/PropertyChangedImplExampleViewModel.cs "Sample")

![DependsOn sample](https://github.com/iirachek/Mvvm-Extensions/tree/master/docs/DependsOnSample.gif)

## DependencyWatcher
`DependencyWatcher` is an alternative class that allows to use the `DependsOn` attribute when its impossible or not desirable to inherit `PropertyChangedImplementation` by the viewmodel. The viewmodel must implement INotifyPropertyChanged and provide `DependencyWatcher` with a method for `PropertyChanged` event invokation.

The resulting behavior of properties marked by `DependsOn` attribute is the same between both methods.

![DependencyWatcher sample](https://github.com/iirachek/Mvvm-Extensions/tree/master/docs/DependencyWatcherSample.gif)

## DelegateCommand
`DelegateCommand` provides the most basic implementation of `ICommand` interface that can be used immediatelly to invoke viewmodel commands from the views.

#### Sample usage
##### DataContext
```
		private bool runningAutomaticOp = false;
		
		public bool RunningAutomaticOp
        {
            get
            {
                return runningAutomaticOp;
            }
            set
            {
                if (runningAutomaticOp != value)
                {
                    runningAutomaticOp = value;
                    NotifyPropertyChanged();
                }
            }
        }
		
        [DependsOn(nameof(RunningAutomaticOp))]
        public ICommand RunAutomaticOperationCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => !RunningAutomaticOp,
                    CommandAction = (x) => RunAutomaticOp()
                };
            }
        }
		
		private void RunAutomaticOp() 
		{
			// Logic
		}
```
##### View
```
<Button Command="{Binding RunAutomaticOperationCommand}" />
```

## CursorState
`CursorState` class allows application to change the widndows cursor icon while it remains in the application window. The cursor automatically reverts back to the default icon when `CursorState` instance is disposed.

#### Sample usage
The following code is used to create behavior visible on the image above:
```
	Application.Current.Dispatcher.Invoke(async () =>
	{
		using (new CursorState(Cursors.Wait))
		{
			await Task.Delay(2000);
		}
	});
```
## Converters
The library contains a number of converters that are nessessary for any decently complex UI.  The available converters and their values are shown below:

#### BooleanToVisibilityConverter
Converts `bool` value to `System.Windows.Visibility`

##### Convert

| Input value | Converted value  |
| ------------ | ------------ |
| true | Visibility.Visible |
| false | Visibility.Collapsed |

##### ConvertBack

| Input value | Converted value  |
| ------------ | ------------ |
| Visibility.Visible | true |
| Visibility.Hidden<br/>Visibility.Collapsed | false |

------------

#### BooleanToVisibilityHiddenConverter
Same as `BooleanToVisibilityConverter`, except `false` evaluates to `Visibility.Hidden` instead of `Visibility.Collapsed`

##### Convert

| Input value | Converted value  |
| ------------ | ------------ |
| true | Visibility.Visible |
| false | Visibility.Hidden |

##### ConvertBack

| Input value | Converted value  |
| ------------ | ------------ |
| Visibility.Visible | true |
| Visibility.Hidden<br/>Visibility.Collapsed  | false |
------------

#### InvertBooleanConverter
Inverts value of the passed `bool` value

------------

#### InvertBooleanToVisibilityConverter
Converts `bool` value to `System.Windows.Visibility`

##### Convert

| Input value | Converted value  |
| ------------ | ------------ |
| true | Visibility.Collapsed |
| false | Visibility.Visible |

##### ConvertBack

| Input value | Converted value  |
| ------------ | ------------ |
| Visibility.Visible | false |
| Visibility.Hidden<br/>Visibility.Collapsed  | true |

------------

#### MultiBoolAndConverter
Multi-value converter that accepts multiple `bool` values.

##### Convert
Returns `true` if every passed value is `true`. Otherwise returns `false`.

##### ConvertBack
Not implemented

------------

#### MultiBoolOrConverter
Multi-value converter that accepts multiple `bool` values.

##### Convert
Returns `true` is at least one passed value is `true`. Otherwise returns `false`.

##### ConvertBack
Not implemented

------------

#### NullToVisibilityConverter
Converts any object to `System.Windows.Visibility`

##### Convert
If passed value is `null`, returns `Visibility.Collapsed`. Otherwise returns `Visibility.Visible`.

##### ConvertBack
Not implemented

# License
[MIT LIcense (MIT)](https://github.com/iirachek/Mvvm-Extensions/blob/master/LICENSE "MIT LIcense (MIT)")
