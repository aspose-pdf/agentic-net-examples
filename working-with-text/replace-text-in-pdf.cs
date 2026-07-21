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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create search options – false = case‑insensitive
            TextSearchOptions searchOptions = new TextSearchOptions(false);

            // Create a TextFragmentAbsorber to locate all occurrences of "Confidential"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential", searchOptions);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Replace each found fragment with the new text "Public"
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "Public";
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced text saved to '{outputPath}'.");
    }
}
