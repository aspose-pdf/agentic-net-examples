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

        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the search word in the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);
            absorber.Visit(doc);

            // Replace each found fragment while keeping its original formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceWord;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced \"{searchWord}\" with \"{replaceWord}\" and saved to '{outputPath}'.");
    }
}