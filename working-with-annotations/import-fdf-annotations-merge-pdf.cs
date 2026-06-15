using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // existing PDF
        const string fdfPath   = "data.fdf";       // FDF containing form annotations
        const string outputPath = "merged_output.pdf";

        // Verify source files exist
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
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the FDF stream
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Import annotations from the FDF into the PDF
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the merged document
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}