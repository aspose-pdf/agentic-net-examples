using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateSelectedText
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the text to be rotated (case‑sensitive exact match)
            const string phraseToRotate = "rotate me";

            // Create a TextFragmentAbsorber that will find all occurrences of the phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phraseToRotate);

            // Search the entire document (you could limit to a specific page if needed)
            doc.Pages.Accept(absorber);

            // Rotate each found text fragment by 45 degrees while keeping its position
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // TextState.Rotation is expressed in degrees
                fragment.TextState.Rotation = 45f;
            }

            // Save the modified PDF; no SaveOptions needed for PDF output
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text saved to '{outputPath}'.");
    }
}