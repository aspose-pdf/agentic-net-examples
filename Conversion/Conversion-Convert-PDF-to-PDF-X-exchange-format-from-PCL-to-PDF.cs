using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PCL, intermediate PDF, final PDF/X and conversion log
        const string pclPath   = "input.pcl";
        const string pdfPath   = "intermediate.pdf";
        const string pdfxPath  = "output_pdfx3.pdf";
        const string logPath   = "conversion_log.txt";

        // Verify source file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Source PCL file not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions and convert it to a PDF document
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        using (Document pdfDoc = new Document(pclPath, pclLoadOptions))
        {
            // Save the intermediate PDF (optional, can be omitted if not needed)
            pdfDoc.Save(pdfPath);

            // Convert the PDF document to PDF/X‑3 format.
            // The Convert method writes conversion messages to the specified log file.
            pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X document
            pdfDoc.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X saved to '{pdfxPath}'.");
    }
}