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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that searches for the word "Draft"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Draft");

            // Search the entire document
            absorber.Visit(doc);

            // Rotate each found text fragment by 15 degrees
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Rotation = 15;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated 'Draft' text saved to '{outputPath}'.");
    }
}