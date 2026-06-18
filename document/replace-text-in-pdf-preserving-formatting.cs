using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Word to find and its replacement
        const string searchWord = "oldWord";
        const string replaceWord = "newWord";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified word
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Replace each found occurrence while preserving original formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceWord;
            }

            // Save the modified PDF; the original formatting of the replaced text is retained
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of '{searchWord}' were replaced with '{replaceWord}'. Output saved to '{outputPath}'.");
    }
}