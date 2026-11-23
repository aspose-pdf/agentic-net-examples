using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Desired X coordinate (points from the left edge) for vertical alignment
            double targetX = 100.0;

            // Adjust each fragment's X position while keeping its original Y position
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Position is of type Aspose.Pdf.Text.Position
                fragment.Position = new Position(targetX, fragment.Position.YIndent);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}