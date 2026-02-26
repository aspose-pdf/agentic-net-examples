using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";   // PDF to receive annotations
        const string fdfFilePath    = "annotations.fdf"; // FDF file containing annotations
        const string outputPdfPath  = "output.pdf";  // Resulting PDF with imported annotations

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfFilePath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfFilePath}");
            return;
        }

        try
        {
            // Load the target PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the FDF stream
                using (FileStream fdfStream = File.OpenRead(fdfFilePath))
                {
                    // Import annotations from the FDF stream into the PDF document
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}