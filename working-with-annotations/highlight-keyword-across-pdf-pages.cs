using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string keyword = "yourKeyword"; // replace with the word to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for all occurrences of the keyword in the entire document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
            doc.Pages.Accept(absorber); // apply absorber to all pages

            // Iterate over each found text fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Directly obtain the page that contains the fragment
                Page page = fragment.Page;
                if (page == null)
                    continue; // safety check

                // Get the rectangle that bounds the fragment
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a highlight annotation on that page and rectangle
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Set the highlight color (yellow is common)
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(highlight);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"{keyword}\" have been highlighted and saved to '{outputPath}'.");
    }
}
