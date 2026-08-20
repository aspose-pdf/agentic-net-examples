using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchPhrase = "Exact text to rotate"; // replace with the target text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the specific text fragment
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            // Search on the first page (adjust page index if needed)
            doc.Pages[1].Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // Rotate the first found fragment by 45 degrees
                absorber.TextFragments[1].TextState.Rotation = 45;
            }
            else
            {
                Console.WriteLine("Specified text not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}