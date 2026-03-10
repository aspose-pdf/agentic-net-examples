using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the FDF file containing annotations,
        // and the output PDF that will contain the imported annotations.
        const string pdfPath   = "input.pdf";
        const string fdfPath   = "annotations.fdf";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the FDF file as a read‑only stream.
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Import all annotations from the FDF stream into the PDF document.
                    // The static method belongs to Aspose.Pdf.Annotations.FdfReader.
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the modified document as a PDF.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}