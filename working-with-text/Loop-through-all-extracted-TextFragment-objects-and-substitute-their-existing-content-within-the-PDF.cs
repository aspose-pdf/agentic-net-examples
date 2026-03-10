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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that will collect all text fragments in the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Iterate over each extracted TextFragment and replace its content
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Example substitution – replace the original text with a placeholder
                fragment.Text = "REPLACED";
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}