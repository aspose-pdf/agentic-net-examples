using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplacePhrasePreserveFormatting
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Phrase to find and its replacement
        const string oldPhrase = "old phrase";
        const string newPhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the old phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldPhrase);

            // Accept the absorber on every page (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                doc.Pages[pageNum].Accept(absorber);
            }

            // Iterate over all found text fragments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Capture the original text state
                TextState originalState = new TextState();
                originalState.ApplyChangesFrom(fragment.TextState);

                // Replace the text
                fragment.Text = newPhrase;

                // Re‑apply the original formatting
                fragment.TextState.ApplyChangesFrom(originalState);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Phrase replacement completed. Output saved to '{outputPath}'.");
    }
}