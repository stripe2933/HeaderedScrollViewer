# HeaderedScrollViewer

![A working screenshot](https://user-images.githubusercontent.com/63503910/179910832-e302c691-2809-4c33-ab40-90e490133da3.gif)

Avalonia Attached Property for showing current header in the ScrollViewer.

## How to use

1. Add the file `HeaderedScrollViewer.cs` file into your project.
2. Make element hierarchy as:
- ScrollViewer
  - StackPanel (Orientation="Vertical")
    - // TextBlock (Classes="header") (*header 1) (the comment is intended)
    - Content for header 1
    - TextBlock (Classes="header") (*header 2)
    - Content for header 2
    - TextBlock (Classes="header") (*header 3)
    - Content for header 3
    
```xaml
<ScrollViewer x:Name="sv"
              HorizontalScrollBarVisibility="Disabled" 
              >
    <StackPanel Orientation="Vertical" Spacing="12">
        <!-- No header text for first header; the CurrentHeaderText will be null at this position. -->
        <Button>Header 1 Item</Button>
        
        <TextBlock Classes="header">Header 2</TextBlock>
        <ComboBox SelectedIndex="0">
            <ComboBoxItem>Header 2 Item - 1</ComboBoxItem>
            <ComboBoxItem>Header 2 Item - 2</ComboBoxItem>
            <ComboBoxItem>Header 2 Item - 3</ComboBoxItem>
        </ComboBox>
        
        <TextBlock Classes="header">Header 3</TextBlock>
        <TextBox Watermark="Header 3 Item"/>
        
        <TextBlock Classes="header">Header 4</TextBlock>
        <StackPanel Orientation="Vertical" HorizontalAlignment="Left" Spacing="8">
            <Ellipse Fill="Red" Width="60" Height="60" HorizontalAlignment="Left"/>
            <Polygon Fill="Blue" Points="30,0 0,60 60,60" HorizontalAlignment="Left"/>
            <Rectangle Fill="Orange" Width="60" Height="120" HorizontalAlignment="Left"/>
        </StackPanel>
    </StackPanel>
</ScrollViewer>
```

3. Attach `HeaderedScrollViewer` to the ScrollViewer and set its TargetHeader as the classname that your marked your header TextBlock. I set the classname as `header`.

```xaml
<ScrollViewer ... local:HeaderedScrollViewer.TargetHeader="header"> ...
```

4. Use `CurrentHeaderText` property in other controls, such as 
```xaml
<TextBlock Text="{Binding #sv.(local:HeaderedScrollViewer.CurrentHeaderText), TargetNullValue='Header 1'}"/> <!-- set the TargetNullValue as the first header that intentionally deleted -->
```

## Example

The source code is in solution file.

`HeaderedScrollViewer.cs` (inside `View` folder)

`MainWindow.xaml` (inside `View` folder)
```xaml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:HeaderedScrollViewer.ViewModels"
        xmlns:local="using:HeaderedScrollViewer.Views"
        Width="320" Height="400"
        x:Class="HeaderedScrollViewer.Views.MainWindow"
        x:CompileBindings="True"
        x:DataType="vm:MainWindowViewModel"
        Icon="/Assets/avalonia-logo.ico"
        Title="HeaderedScrollViewer">
    <Window.Styles>
        <Style Selector="TextBlock.main-header">
            <Setter Property="FontSize" Value="32"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
        <Style Selector="TextBlock.header">
            <Setter Property="FontSize" Value="24"/>
            <Setter Property="FontWeight" Value="DemiBold"/>
        </Style>
    </Window.Styles>

    <Design.DataContext>
        <vm:MainWindowViewModel/>
    </Design.DataContext>

    <DockPanel Margin="12">
        <!-- Set Text binding's TargetNullValue as the first header text. -->
        <TextBlock DockPanel.Dock="Top" 
                   Classes="main-header"
                   Text="{Binding #sv.(local:HeaderedScrollViewer.CurrentHeaderText), TargetNullValue='Header 1'}"
                   Margin="0,0,0,8"/>
        
        <ScrollViewer x:Name="sv"
                      HorizontalScrollBarVisibility="Disabled" 
                      local:HeaderedScrollViewer.TargetHeader="header"
            <StackPanel Orientation="Vertical" Spacing="12">
                <!-- No header text for first header; the CurrentHeaderText will be null at this position. -->
                <Button>Header 1 Item</Button>
                
                <TextBlock Classes="header">Header 2</TextBlock>
                <ComboBox SelectedIndex="0">
                    <ComboBoxItem>Header 2 Item - 1</ComboBoxItem>
                    <ComboBoxItem>Header 2 Item - 2</ComboBoxItem>
                    <ComboBoxItem>Header 2 Item - 3</ComboBoxItem>
                </ComboBox>
                
                <TextBlock Classes="header">Header 3</TextBlock>
                <TextBox Watermark="Header 3 Item"/>
                
                <TextBlock Classes="header">Header 4</TextBlock>
                <StackPanel Orientation="Vertical" HorizontalAlignment="Left" Spacing="8">
                    <Ellipse Fill="Red" Width="60" Height="60" HorizontalAlignment="Left"/>
                    <Polygon Fill="Blue" Points="30,0 0,60 60,60" HorizontalAlignment="Left"/>
                    <Rectangle Fill="Orange" Width="60" Height="120" HorizontalAlignment="Left"/>
                </StackPanel>
            </StackPanel>
        </ScrollViewer>
    </DockPanel>
</Window>

```

The result is same as the above GIF file.

### Usage Example

[Todo.Avalonia](https://github.com/stripe2933/Todo.Avalonia)
![Todo.Avalonia](https://user-images.githubusercontent.com/63503910/180939386-40ff8574-cfb2-4260-9821-e0ccfa9a700f.gif)
