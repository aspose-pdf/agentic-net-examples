using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to search for and its replacement
        const string phraseToReplace = "hello world";
        const string replacementText = "hi world";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Instantiate TextFragmentAbsorber with the phrase to be replaced
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phraseToReplace);

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Replace each found occurrence with the new text
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replacementText;
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
    }
}