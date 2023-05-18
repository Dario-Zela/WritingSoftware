using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.CustomControls
{
    /// <summary>
    /// Interaction logic for RichTextBox.xaml
    /// </summary>
    public partial class RichTextBox : UserControl
    {
        public RichTextBox()
        {
            InitializeComponent();
            DataContext = this;
            FontFamilySetter.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontSizeSetter.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        }

        public bool TooltipOpen
        {
            get { return (bool)GetValue(TooltipOpenProperty); }
            set { SetValue(TooltipOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TooltipOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TooltipOpenProperty =
            DependencyProperty.Register("TooltipOpen", typeof(bool), typeof(RichTextBox), new PropertyMetadata(false));


        public bool SpellCheck
        {
            get { return (bool)GetValue(SpellCheckProperty); }
            set { SetValue(SpellCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpellCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpellCheckProperty =
            DependencyProperty.Register("SpellCheck", typeof(bool), typeof(RichTextBox), new PropertyMetadata(true));



        private void FontFamilyChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilySetter.SelectedItem == null) return;
            TextEditor.Selection?.ApplyPropertyValue(FontFamilyProperty, FontFamilySetter.SelectedItem);
        }

        private void FontSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilySetter.SelectedItem == null) return;
            TextEditor.Selection?.ApplyPropertyValue(FontSizeProperty, FontSizeSetter.SelectedItem);
        }

        private void StrikeOutButton_Click(object sender, RoutedEventArgs e)
        {
            Through.IsChecked = !Through.IsChecked.Value;
            TextRange textRange = TextEditor.Selection;
            var decorations = textRange.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection
                              ?? new TextDecorationCollection();

            SanityCheck();
            decorations = decorations.Contains(TextDecorations.Strikethrough.First())
                              ? new TextDecorationCollection(decorations.Except(TextDecorations.Strikethrough))
                              : new TextDecorationCollection(decorations.Union(TextDecorations.Strikethrough));

            textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, decorations);
            Through.IsChecked = !Through.IsChecked.Value;
        }

        private void SanityCheck()
        {
            TextRange textRange = TextEditor.Selection;
            var decorations = textRange.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection
                              ?? new TextDecorationCollection();

            if (decorations.Contains(TextDecorations.Strikethrough.First()) != Through.IsChecked.Value)
            {
                decorations.Clear();
                if (Underline.IsChecked.Value)
                    decorations.Add(TextDecorations.Underline.First());
                if (Through.IsChecked.Value)
                    decorations.Add(TextDecorations.Strikethrough.First());
            }
        }

        private List FindListAncestor(DependencyObject element)
        {
            while (element != null)
            {
                List list = element as List;
                if (list != null)
                {
                    return list;
                }

                element = LogicalTreeHelper.GetParent(element);
            }

            return null;
        }

        private void TextEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TooltipOpen = false;
            var currentSelectionPosition = TextEditor.Selection.Start.GetCharacterRect(LogicalDirection.Forward);
            currentSelectionPosition.Offset(new Vector(Tooltip.Width / 2, 0));
            Tooltip.PlacementRectangle = currentSelectionPosition;

            TooltipOpen = TextEditor.Selection.Text.Length != 0;

            object text = TextEditor.Selection.GetPropertyValue(FontWeightProperty);
            Bold.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(FontWeights.Bold));

            text = TextEditor.Selection.GetPropertyValue(FontStyleProperty);
            Italic.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(FontStyles.Italic));

            text = TextEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underline.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(TextDecorations.Underline));
            Underline.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(TextDecorations.Strikethrough));

            text = FindListAncestor(TextEditor.Selection.Start.Parent);
            DottedList.IsChecked = (text != null) && (((List)text).MarkerStyle == TextMarkerStyle.Circle);
            NumberedList.IsChecked = (text != null) && (((List)text).MarkerStyle == TextMarkerStyle.Decimal);

            text = TextEditor.Selection.GetPropertyValue(FontFamilyProperty);
            FontFamilySetter.SelectedItem = text;

            text = TextEditor.Selection.GetPropertyValue(FontSizeProperty);
            FontSizeSetter.SelectedItem = text;
        }

        private void NumberedList_Click(object sender, RoutedEventArgs e)
        {
            DottedList.IsChecked = false;
        }

        private void DottedList_Click(object sender, RoutedEventArgs e)
        {
            NumberedList.IsChecked = false;
        }
    }
}
