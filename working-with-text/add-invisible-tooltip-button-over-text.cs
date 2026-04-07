using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tooltip.pdf";
        const string tooltip = "This is a tooltip for the highlighted text.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Locate the target text on the first page (adjust as needed)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("target text");
            doc.Pages[1].Accept(absorber);
            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("Target text not found.");
                return;
            }
            TextFragment fragment = absorber.TextFragments[0];

            // Determine the rectangle that bounds the found text
            // Use the Rectangle property of TextFragment (GetRectangle does not exist)
            Aspose.Pdf.Rectangle textRect = fragment.Rectangle;

            // Create an invisible button field over the text rectangle
            ButtonField btn = new ButtonField(doc, textRect);
            btn.AlternateName = tooltip;                     // Tooltip shown on hover
            btn.Border = new Border(btn) { Width = 0 };      // No visible border
            btn.Color = Aspose.Pdf.Color.Transparent;       // No fill color
            // Make the annotation invisible (optional, tooltip still works)
            btn.Flags = AnnotationFlags.Invisible;           // Correct enum assignment

            // Add the button field to the document's form
            doc.Form.Add(btn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip button: {outputPath}");
    }
}
