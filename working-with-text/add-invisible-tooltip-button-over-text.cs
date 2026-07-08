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
        const string inputPath = "input.pdf";
        const string outputPath = "output_tooltip.pdf";
        const string searchText = "Sample Text";          // text to locate
        const string tooltip = "This is a tooltip text"; // tooltip to show

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the first occurrence of the target text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("Text not found in the document.");
                return;
            }

            // Get the rectangle of the found fragment (1‑based indexing)
            TextFragment fragment = absorber.TextFragments[1];
            Aspose.Pdf.Rectangle rect = fragment.Rectangle;

            // Determine the page where the fragment resides
            int pageNumber = fragment.Page.Number; // Use fragment.Page.Number

            // Create an invisible button field over the text rectangle
            ButtonField button = new ButtonField(doc, rect);
            button.PartialName = "tooltipBtn";          // unique field identifier
            button.AlternateName = tooltip;               // tooltip text shown by PDF viewers
            button.Flags = AnnotationFlags.Invisible;    // make it invisible
            button.Border = new Border(button) { Width = 0 }; // no visible border

            // Add the button to the form on the correct page
            doc.Form.Add(button, pageNumber);
            // Ensure the button has an appearance (required for some viewers)
            doc.Form.AddFieldAppearance(button, pageNumber, rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip button: {outputPath}");
    }
}
