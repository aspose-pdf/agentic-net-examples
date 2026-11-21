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
            // Create a TextFragmentAbsorber that searches for the word "Draft"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Draft");

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over each found TextFragment and rotate it by 15 degrees
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Rotation = 15; // rotation in degrees
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}