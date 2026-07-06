using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Phrase to find and its replacement
        const string searchPhrase = "hello world";
        const string replacementText = "hi universe";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on all pages (Pages collection is 1‑based)
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment and replace its text while preserving formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replacementText; // original TextState (font, size, color, etc.) is kept
            }

            // Save the modified document (save without explicit options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced \"{searchPhrase}\" with \"{replacementText}\" and saved to \"{outputPath}\".");
    }
}
