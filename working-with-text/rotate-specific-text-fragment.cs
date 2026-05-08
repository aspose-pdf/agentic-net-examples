using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchText = "Target Text"; // text fragment to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Find the desired text fragment on the first page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[1].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("Text not found.");
            }
            else
            {
                // Rotate the first occurrence by 45 degrees
                TextFragment fragment = absorber.TextFragments[1];
                fragment.TextState.Rotation = 45; // rotation in degrees
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}