using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceWordInPdf
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Word to replace and its replacement
        const string oldWord = "hello";
        const string newWord = "hi";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, perform replacement, and save
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that searches for the target word
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldWord);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Replace each found occurrence while keeping original formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = newWord; // formatting (font, size, color) is preserved
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Word replacement completed. Output saved to '{outputPath}'.");
    }
}
