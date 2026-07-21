using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // existing PDF
        const string fdfPath   = "data.fdf";       // FDF file with annotations
        const string outputPath = "merged_output.pdf";

        // Verify files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open the FDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations from the FDF into the PDF document
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                // Stream will be closed automatically by the using block
            }

            // Save the updated PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"FDF annotations merged and saved to '{outputPath}'.");
    }
}