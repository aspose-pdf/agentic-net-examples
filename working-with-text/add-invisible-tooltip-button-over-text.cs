using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";
        const string searchText = "Sample Text";          // text to locate
        const string tooltip    = "This is a tooltip message";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first occurrence of the target text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);
            TextFragmentCollection fragments = absorber.TextFragments;

            if (fragments.Count == 0)
            {
                Console.Error.WriteLine($"Text \"{searchText}\" not found.");
                return;
            }

            // Get the rectangle of the found text fragment
            TextFragment tf = fragments[0];
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle textRect = tf.Rectangle;

            // Create an invisible button field over the text rectangle
            ButtonField btn = new ButtonField(doc, textRect);
            // AlternateName is used as the tooltip text in Acrobat
            btn.AlternateName = tooltip;

            // Make the button invisible
            btn.Border = new Border(btn) { Width = 0 };
            btn.Color = Aspose.Pdf.Color.Transparent;

            // Assign a JavaScript action that shows a tooltip (alert) when the mouse enters the button area
            btn.Actions.OnEnter = new JavascriptAction($"app.alert('{tooltip}');");

            // Add the button to the document's form
            doc.Form.Add(btn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip button: {outputPath}");
    }
}