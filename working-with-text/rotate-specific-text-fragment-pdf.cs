using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_fragment.pdf";
        const string searchText = "Target Text"; // replace with the exact text to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use constructor and using block)
        using (Document doc = new Document(inputPath))
        {
            // Find the specific text fragment using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[1].Accept(absorber); // search on the first page (adjust if needed)

            if (absorber.TextFragments.Count > 0)
            {
                // Rotate the first matching fragment by 45 degrees
                absorber.TextFragments[1].TextState.Rotation = 45;
            }
            else
            {
                Console.WriteLine("No matching text fragment found.");
            }

            // Save the modified document (lifecycle rule: use Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}