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
        const string searchWord = "oldWord";
        const string replaceWord = "newWord";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Find all occurrences of the target word on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);

                // Configure search options – only the constructor argument is required.
                // The boolean indicates whether to search in annotations; we set it to true.
                TextSearchOptions searchOptions = new TextSearchOptions(true);
                absorber.TextSearchOptions = searchOptions;

                // Apply the absorber to the current page
                doc.Pages[pageNum].Accept(absorber);

                // Replace each found fragment while preserving its original formatting
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceWord; // only the text changes; font, size, color stay the same
                }
            }

            // Save the modified document; without SaveOptions this writes a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced all \"{searchWord}\" with \"{replaceWord}\" and saved to \"{outputPath}\".");
    }
}
