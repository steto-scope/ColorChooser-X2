# ColorChooser-X2

ColorChooser X2 is a Color Picker Control für WPF/.NET written in C#. 


## Features

* MVVM Support
* including 2 Themes: Default and Dark
* Sample App


## Using it

It is possible to use the Control the MVVM-way or the classic way.

The Main Control is ChooserX2 in the ColorChooserX2.Views-Namepsace. When an instance of it is created, it will get the Standard-ViewModel (HSVFieldViewModel) automatically. 

### Accessing the Colors

The ChooserX2-Instance has a ViewModel in it´s DataContext that implements the IColorChooser-Interface. So you´re able to access the currently selected (or hovered) color by

    IColorChooser vm = chooserx2control.DataContext as IColorChooser;
    if(vm!=null)
    {
        Color selected = vm.SelectedColor;
        Color hovered = vm.HoverColor;
    } 

You can register to the SelectedColor/HoverColor-Changed Event to listen for changes. Alternatively you can of course bind to The SelectedColor/HoverColor Property (like in the sample App). The Binding works TwoWay, so changes of the Members updates the GUI too.


## License

LGPLv3
