using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchPhrase = "hello";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to search for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Iterate over found fragments (if any)
            int idx = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Fragment {idx}: \"{fragment.Text}\" on page {fragment.Page.Number}");
                // Example modification: replace the found text with uppercase version
                fragment.Text = fragment.Text.ToUpperInvariant();
                idx++;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Search completed. Modified PDF saved to '{outputPath}'.");
    }
}