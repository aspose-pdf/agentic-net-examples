using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string fdfPath   = "annotations.fdf"; // FDF containing annotations
        const string outputPath = "output.pdf";    // result PDF

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
            // Load the PDF document (creation & loading)
            using (Document doc = new Document(pdfPath))
            {
                // Open the FDF stream and import annotations.
                // The FDF file contains page numbers, so Aspose.Pdf places each annotation on the correct page automatically.
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the updated PDF (saving)
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