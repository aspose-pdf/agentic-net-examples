using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplacePhraseWithFormatting
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Phrase to search for and its replacement
        const string searchPhrase = "hello world";
        const string replacementPhrase = "hi universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Accept the absorber on each page (you can also target a single page)
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Iterate over all found text fragments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Preserve the original formatting by copying its TextState
                TextState originalState = new TextState();
                originalState.ApplyChangesFrom(fragment.TextState);

                // Replace the text
                fragment.Text = replacementPhrase;

                // Re‑apply the original formatting to the modified fragment
                fragment.TextState.ApplyChangesFrom(originalState);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Phrase replacement completed. Output saved to '{outputPath}'.");
    }
}