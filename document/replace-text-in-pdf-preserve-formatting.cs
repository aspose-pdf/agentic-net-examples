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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to find all occurrences of the target word
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Replace each found fragment's text while keeping its original formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceWord;
            }

            // Save the modified PDF (saving without options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced '{searchWord}' with '{replaceWord}' and saved to '{outputPath}'.");
    }
}