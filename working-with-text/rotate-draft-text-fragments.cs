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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the core Document API (no Facades)
        using (Document doc = new Document(inputPath))
        {
            // Locate every text fragment that contains the word "Draft"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Draft");
            doc.Pages.Accept(absorber);

            // Apply a 15‑degree rotation to each matching fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Rotation = 15; // degrees
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
