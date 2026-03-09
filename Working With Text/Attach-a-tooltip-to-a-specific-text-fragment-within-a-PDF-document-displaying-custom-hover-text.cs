using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";
        const string searchText = "Target Phrase";      // text to attach the tooltip to
        const string tooltip    = "Custom hover text displayed as a tooltip.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the specific text fragment on the first page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[1].Accept(absorber);

            // Ensure at least one occurrence was found
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("Specified text not found in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Get the rectangle of the first found fragment
            TextFragment fragment = absorber.TextFragments[1];
            Aspose.Pdf.Rectangle rect = fragment.Rectangle;

            // Create a TextAnnotation (sticky note) positioned over the fragment
            TextAnnotation tooltipAnnotation = new TextAnnotation(doc.Pages[1], rect)
            {
                // Title appears in the annotation's title bar (optional)
                Title = "Info",
                // Contents is the text shown in the tooltip/pop‑up
                Contents = tooltip,
                // Set a subtle color for the annotation icon
                Color = Aspose.Pdf.Color.Yellow,
                // Do not open the annotation by default; it will appear on hover
                Open = false,
                // Choose an icon style (e.g., Note)
                Icon = TextIcon.Note
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(tooltipAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip at '{outputPath}'.");
    }
}