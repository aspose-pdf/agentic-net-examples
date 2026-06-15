using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // needed for AnnotationFlags and Border

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "Target Text";      // text to locate
        const string tooltip = "This is a tooltip";   // tooltip to show

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the text and obtain its rectangle
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("Text not found.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Use the first occurrence
            TextFragment fragment = absorber.TextFragments[0];
            Aspose.Pdf.Rectangle textRect = fragment.Rectangle;

            // Create an invisible button field over the text rectangle
            ButtonField btn = new ButtonField(doc, textRect);
            btn.AlternateName = tooltip;               // tooltip text (shown on hover)
            btn.ReadOnly = true;                       // make it non‑editable

            // Make the button invisible
            btn.Border = new Border(btn) { Width = 0 };
            btn.Color = Aspose.Pdf.Color.Transparent;
            btn.Flags = AnnotationFlags.Invisible; // correct enum assignment

            // Add the button to the form
            doc.Form.Add(btn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip button: {outputPath}");
    }
}
