using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragmentAbsorber resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all text (including any hidden text)
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.RemoveAllText(doc);

                // Remove document metadata
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance if present
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Flatten transparency to eliminate hidden transparent objects
                doc.FlattenTransparency();

                // Optimize resources to discard unused objects
                doc.OptimizeResources();

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Hidden content removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}