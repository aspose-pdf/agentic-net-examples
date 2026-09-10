using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceWordInPdf
{
    static void Main()
    {
        // Input PDF path, word to replace, and replacement word
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchWord = "hello";
        const string replaceWord = "hi";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the target word.
            // No TextEditOptions are required for simple replacement; the absorber
            // will retain the original TextState (font, size, color, etc.) of each fragment.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);

            // Search all pages of the document
            doc.Pages.Accept(absorber);

            // Replace each found occurrence with the new word.
            // The TextFragment retains its original formatting automatically.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceWord;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Word replacement completed. Output saved to '{outputPath}'.");
    }
}
