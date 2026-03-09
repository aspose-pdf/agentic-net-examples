using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase     = "hello world"; // text to locate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Search for the specified phrase on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            doc.Pages.Accept(absorber);

            // Apply a new font color to each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Example: change text color to blue
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 1);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}