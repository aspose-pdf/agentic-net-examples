using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all text (including any hidden or invisible text)
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.RemoveAllText(doc);

                // Flatten transparent objects so that hidden graphics become visible or are removed
                doc.FlattenTransparency();

                // Optimize resources to discard any now‑unused objects
                doc.OptimizeResources();

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}