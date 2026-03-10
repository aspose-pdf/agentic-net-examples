using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the text "Hello" on the first page (1‑based indexing)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Hello");
            doc.Pages[1].Accept(absorber);

            // If at least one occurrence is found, modify its font properties
            if (absorber.TextFragments.Count > 0)
            {
                // Retrieve the Arial font and mark it for embedding
                Font font = FontRepository.FindFont("Arial");
                font.IsEmbedded = true;

                // Apply font, size, and color to the first found fragment
                absorber.TextFragments[1].TextState.Font = font;
                absorber.TextFragments[1].TextState.FontSize = 14;
                absorber.TextFragments[1].TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font properties updated and saved to '{outputPath}'.");
    }
}