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
        const string searchWord = "oldWord";   // word to replace
        const string replaceWord = "newWord"; // replacement word

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the target word
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Replace each found occurrence while preserving its original formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceWord;
            }

            // Save the modified PDF (output format is PDF, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"{searchWord}\" have been replaced with \"{replaceWord}\" and saved to \"{outputPath}\".");
    }
}