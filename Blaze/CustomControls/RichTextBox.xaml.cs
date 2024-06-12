using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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
            //Sets all of the font families and allowed font sizes
            FontFamilySetter.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontSizeSetter.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }


        //Current state of the tooltip
        public bool TooltipOpen
        {
            get { return (bool)GetValue(TooltipOpenProperty); }
            set { SetValue(TooltipOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TooltipOpen. 
        public static readonly DependencyProperty TooltipOpenProperty =
            DependencyProperty.Register("TooltipOpen", typeof(bool), typeof(RichTextBox), new PropertyMetadata(false));


        //Current state of the spellchecker
        public bool SpellCheck
        {
            get { return (bool)GetValue(SpellCheckProperty); }
            set { SetValue(SpellCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpellCheck. 
        public static readonly DependencyProperty SpellCheckProperty =
            DependencyProperty.Register("SpellCheck", typeof(bool), typeof(RichTextBox), new PropertyMetadata(true));



        //Applies font family change
        private void FontFamilyChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilySetter.SelectedItem == null) return;
            TextEditor.Selection?.ApplyPropertyValue(FontFamilyProperty, FontFamilySetter.SelectedItem);
        }

        //Applies font size change
        private void FontSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilySetter.SelectedItem == null) return;
            TextEditor.Selection?.ApplyPropertyValue(FontSizeProperty, FontSizeSetter.SelectedItem);
        }

        //Applies strikethrough
        private void StrikeOutButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the decorations
            TextRange textRange = TextEditor.Selection;
            var decorations = textRange.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection
                              ?? new TextDecorationCollection();

            //If nothing exists add strikethrough
            if (Through.IsChecked.Value && decorations == null)
            {
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
            //If something exist add to the existing decorations
            else if (Through.IsChecked.Value && decorations != null)
            {
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, new TextDecorationCollection(decorations.Union(TextDecorations.Strikethrough)));
            }
            //Else, if we remove the strikethrough, to avoid "ghost" entities, remove all decorations on the strikethrough location
            else
            {
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, new TextDecorationCollection(decorations.Where((d) => { return d.Location != TextDecorationLocation.Strikethrough; })));
            }
        }

        //Finds all the ancestors of an element
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

        //Applies all other text decorators
        private void TextEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Opens the ribbon at correct position
            TooltipOpen = false;
            var currentSelectionPosition = TextEditor.Selection.Start.GetCharacterRect(LogicalDirection.Forward);
            currentSelectionPosition.Offset(new Vector(Tooltip.Width / 2, 0));
            Tooltip.PlacementRectangle = currentSelectionPosition;

            TooltipOpen = TextEditor.Selection.Text.Length != 0;

            //Applies bold
            object text = TextEditor.Selection.GetPropertyValue(FontWeightProperty);
            Bold.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(FontWeights.Bold));

            //Applies italic
            text = TextEditor.Selection.GetPropertyValue(FontStyleProperty);
            Italic.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(FontStyles.Italic));

            //Applies underline
            text = TextEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underline.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(TextDecorations.Underline));
            Underline.IsChecked = (text != DependencyProperty.UnsetValue) && (text.Equals(TextDecorations.Strikethrough));

            //Applies list types
            text = FindListAncestor(TextEditor.Selection.Start.Parent);
            DottedList.IsChecked = (text != null) && (((List)text).MarkerStyle == TextMarkerStyle.Circle);
            NumberedList.IsChecked = (text != null) && (((List)text).MarkerStyle == TextMarkerStyle.Decimal);

            //Applies font family
            text = TextEditor.Selection.GetPropertyValue(FontFamilyProperty);
            FontFamilySetter.SelectedItem = text;

            //Applies font size
            text = TextEditor.Selection.GetPropertyValue(FontSizeProperty);
            FontSizeSetter.SelectedItem = text;
        }


        //Changes state of the list buttons on the ribbon 

        private void NumberedList_Click(object sender, RoutedEventArgs e)
        {
            DottedList.IsChecked = false;
        }

        private void DottedList_Click(object sender, RoutedEventArgs e)
        {
            NumberedList.IsChecked = false;
        }


        public void Load(Block[] blocks)
        {
            TextEditor.Document.Blocks.Clear();
            TextEditor.Document.Blocks.AddRange(blocks);
        }

        public Block[] GetBlocks()
        {
            Block[] blocks = new Block[TextEditor.Document.Blocks.Count];
            TextEditor.Document.Blocks.CopyTo(blocks, 0);
            return blocks;
        }
    }
}
