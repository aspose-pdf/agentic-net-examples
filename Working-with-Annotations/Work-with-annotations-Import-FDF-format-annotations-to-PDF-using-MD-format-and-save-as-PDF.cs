using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Provides FdfReader for FDF import

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Source PDF
        const string fdfPath = "annotations.fdf";    // FDF file containing annotations
        const string outputPath = "output.pdf";      // Resulting PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Open the FDF stream and import its annotations into the PDF
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}