using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateTextFragments
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchPhrase = "rotate me"; // text to rotate
        const float rotationAngle = 45f; // degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the target phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            doc.Pages[1].Accept(absorber); // search on the first page (adjust as needed)

            // Apply rotation to each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Rotation is defined in degrees; positive values rotate clockwise
                fragment.TextState.Rotation = rotationAngle;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text saved to '{outputPath}'.");
    }
}