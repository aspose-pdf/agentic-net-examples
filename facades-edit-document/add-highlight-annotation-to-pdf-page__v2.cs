using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string searchText = "SELECTED TEXT"; // replace with the exact text to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Search for the text on page 3 only (page index is 1‑based)
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
        doc.Pages[3].Accept(absorber);

        // If nothing was found, exit gracefully
        if (absorber.TextFragments.Count == 0)
        {
            Console.WriteLine("No matching text found on page 3.");
            return;
        }

        // Add a highlight annotation for each found fragment
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            // Create a HighlightAnnotation on the same page using the fragment rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[3], fragment.Rectangle);
            highlight.Color = Color.Yellow; // set highlight color
            doc.Pages[3].Annotations.Add(highlight);
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Highlight annotation applied. Output saved to '{outputPath}'.");
    }
}