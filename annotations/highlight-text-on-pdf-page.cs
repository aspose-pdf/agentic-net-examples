using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string searchText = "sample text to highlight"; // text to find on page 2

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document does not contain a page 2.");
                return;
            }

            // Search for the desired text on page 2
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[2].Accept(absorber);

            // If the text is not found, exit gracefully
            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine($"Text \"{searchText}\" not found on page 2.");
                return;
            }

            // Use the first occurrence's rectangle for the highlight annotation
            TextFragment fragment = absorber.TextFragments[1];
            Aspose.Pdf.Rectangle rect = fragment.Rectangle;

            // Create a highlight annotation with the found rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[2], rect)
            {
                // Set the highlight color (yellow is typical)
                Color = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page
            doc.Pages[2].Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}