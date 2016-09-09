using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;

namespace WpfRichTextBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Regex keywordone = new Regex( @"@(\w+)");
        public Regex keywordtwo = new Regex(@"#(\w+)");
        //public Regex keywordtwo = new Regex("/^(?!.*\bRT\b)(?:.+\s)?#\w+/i");
        private string value = string.Empty;

        public MainWindow()
        {
            InitializeComponent();


        }
            
        private void richtextbox1_TextChanged(object sender, TextChangedEventArgs e)
{

TextChange change = (from x in e.Changes where
x.Offset == e.Changes.Max(p => p.Offset)
select x).FirstOrDefault();


if (change != null && change.Offset != 0)
{

TextRange range = new TextRange(this.richtextbox1.Document
.ContentStart.GetPositionAtOffset(change.Offset - 1), this.richtextbox1.Document.ContentStart.GetPositionAtOffset(change.Offset));


if (range.Text != " ")
{
if (change.AddedLength > 0)
{
value += range.Text;
}
else if (change.RemovedLength > 0)
{
if (value.Length != 0)
{
value = value.Remove(value.Length - 1);
}
else
value = string.Empty;
}


TryColorSyntax(value, change);
}
else
value = string.Empty;
}
}


private void TryColorSyntax(string value, TextChange current)
{
   

    try { 
    richtextbox1.TextChanged -= this.richtextbox1_TextChanged;

TextRange r = new TextRange(this.richtextbox1.Document.ContentStart
.GetPositionAtOffset(current.Offset - 1 - value.Length)
, this.richtextbox1.Document.ContentStart
.GetPositionAtOffset(current.Offset));



if (keywordone.IsMatch(value) || keywordtwo.IsMatch(value)  && value.Length > 0)
{
    
r.ApplyPropertyValue(TextElement.ForegroundProperty,
new SolidColorBrush(Colors.Red));
}    
else
{   //default color is green
r.ApplyPropertyValue(TextElement.ForegroundProperty,
new SolidColorBrush(Colors.Green));
}


richtextbox1.TextChanged += this.richtextbox1_TextChanged;

        }
    catch { }

}

private void richtextbox1_KeyUp(object sender, KeyEventArgs e)
{

}

        }
       
    }
