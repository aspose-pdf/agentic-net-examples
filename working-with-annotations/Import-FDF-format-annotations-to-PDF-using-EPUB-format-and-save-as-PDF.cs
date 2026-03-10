using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string epubPath   = "input.epub";   // EPUB source to be converted to PDF
        const string fdfPath    = "annotations.fdf"; // FDF file containing annotations
        const string outputPdf  = "output.pdf";   // Resulting PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load EPUB as a PDF document using EpubLoadOptions
            using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Import annotations from the FDF file into the document
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the modified document as a PDF file
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF created with imported annotations: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}