using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace HeaderedScrollViewer.Views;

public class HeaderedScrollViewer : AvaloniaObject
{
    #region Fields

    private static List<TextBlock>? _headerTexts;

    #endregion

    #region Avalonia Properties
    
    /// <summary>
    /// The classname to mark the TextBlock as header text.
    /// </summary>
    public static readonly AttachedProperty<string> TargetHeaderProperty =
        AvaloniaProperty.RegisterAttached<Views.HeaderedScrollViewer, ScrollViewer, string>("TargetHeader");
    public static void SetTargetHeader(AvaloniaObject element, string value) => 
        element.SetValue(TargetHeaderProperty, value);
    public static string GetTargetHeader(AvaloniaObject element) => 
        element.GetValue(TargetHeaderProperty);

    /// <summary>
    /// Current header text; null if no current header text
    /// </summary>
    public static readonly AttachedProperty<string?> CurrentHeaderTextProperty =
        AvaloniaProperty.RegisterAttached<Views.HeaderedScrollViewer, ScrollViewer, string?>("CurrentHeaderText");
    public static void SetCurrentHeaderText(AvaloniaObject element, string? value) => 
        element.SetValue(CurrentHeaderTextProperty, value);
    public static string? GetCurrentHeaderText(AvaloniaObject element) => 
        element.GetValue(CurrentHeaderTextProperty);
    
    #endregion

    static HeaderedScrollViewer()
    {
        TargetHeaderProperty.Changed.Subscribe(x =>
            HandleTargetHeaderChanged(x.Sender, x.NewValue.GetValueOrDefault() ?? string.Empty));
    }
    
    /// <summary>
    /// Get TextBlocks which are marked as header text.
    /// </summary>
    /// <param name="scrollViewer">A ScrollViewer that has StackPanel for its contents and the StackPanel has headers inside.</param>
    /// <param name="targetHeader">Classname for header text mark.</param>
    /// <returns>A lists of Header TextBlock. If nothing found or scrollViewer does not have StackPanel for its content, return null.</returns>
    private static IEnumerable<TextBlock>? GetHeaderTextBlocks(ScrollViewer scrollViewer, string targetHeader)
    {
        if (scrollViewer.Content is StackPanel stackPanel)
        {
            return stackPanel.Children
                .Where(p => p is TextBlock textBlock && textBlock.Classes.Contains(targetHeader))
                .Cast<TextBlock>();
        }

        return null;
    }

    
    private static void HandleTargetHeaderChanged(IAvaloniaObject element, string value)
    {
        if (element is ScrollViewer scrollViewer)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Add non-null value
                scrollViewer.AddHandler(ScrollViewer.ScrollChangedEvent, Handler);
            }
            else
            {
                // remove prev value
                scrollViewer.RemoveHandler(ScrollViewer.ScrollChangedEvent, Handler);
            }
        }

        // Executed when the ScrollViewer scroll changed.
        void Handler(object? s, ScrollChangedEventArgs e)
        {
            // Get header TextBlocks in scrollViewer if _headerTexts is null
            if (_headerTexts == null)
            {
                var textEnumerables = GetHeaderTextBlocks(scrollViewer, value);
                _headerTexts = textEnumerables != null ? new List<TextBlock>(textEnumerables) : null;
            }
            
            // Find the current header TextBlock which meets the condition.
            var currentHeaderTextBlock = _headerTexts?.LastOrDefault(p =>
            {
                var transform = p.TransformToVisual(scrollViewer);
                double y = new Point().Transform(transform ?? Matrix.Identity).Y;

                return y <= -p.Bounds.Height;
            });
            string? currentHeaderText = currentHeaderTextBlock?.Text;
                    
            SetCurrentHeaderText(scrollViewer, currentHeaderText);
        }
    }
}