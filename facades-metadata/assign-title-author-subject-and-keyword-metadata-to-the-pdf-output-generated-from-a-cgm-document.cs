using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify input CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Convert CGM to PDF using the Facade API
        try
        {
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF production: {ex.Message}");
            return;
        }

        // Open the generated PDF with PdfFileInfo to set metadata
        try
        {
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                pdfInfo.Title    = "Sample PDF Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Demo of CGM to PDF with metadata";
                pdfInfo.Keywords = "CGM, Aspose.Pdf, metadata";

                // Persist the updated metadata back to the file
                pdfInfo.SaveNewInfo(pdfPath);
            }

            Console.WriteLine($"PDF created with metadata at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error setting PDF metadata: {ex.Message}");
        }
    }
}