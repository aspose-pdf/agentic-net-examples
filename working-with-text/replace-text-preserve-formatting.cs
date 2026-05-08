using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Phrase to find and its replacement
        const string oldPhrase = "Hello World";
        const string newPhrase = "Greetings Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create an absorber that searches for the old phrase on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldPhrase);

                // Perform the search on the page
                doc.Pages[pageNum].Accept(absorber);

                // Replace each found fragment while preserving its original formatting
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Capture the original text state (font, size, color, etc.)
                    TextState originalState = new TextState();
                    originalState.ApplyChangesFrom(fragment.TextState);

                    // Replace the text content
                    fragment.Text = newPhrase;

                    // Restore the captured formatting to the modified fragment
                    fragment.TextState.ApplyChangesFrom(originalState);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with replaced text to '{outputPath}'.");
    }
}