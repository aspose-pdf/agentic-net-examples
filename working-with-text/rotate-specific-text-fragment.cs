using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the text to rotate
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string targetText = "RotateMe";   // text fragment to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Find the desired text fragment on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(targetText);
            doc.Pages.Accept(absorber);

            // Ensure at least one occurrence was found
            if (absorber.TextFragments.Count > 0)
            {
                // Rotate the first matching fragment by 45 degrees
                // TextFragment.TextState is a TextFragmentState object which has a Rotation property
                absorber.TextFragments[1].TextState.Rotation = 45;
            }
            else
            {
                Console.WriteLine($"Text \"{targetText}\" not found in the document.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}