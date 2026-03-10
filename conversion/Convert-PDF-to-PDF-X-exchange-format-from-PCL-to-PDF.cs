using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Not required for this task but kept for completeness

class Program
{
    static void Main()
    {
        // Input PCL file and desired output PDF/X file paths
        const string pclPath      = "input.pcl";
        const string pdfxPath     = "output_pdfx3.pdf";
        const string conversionLog = "conversion_log.txt";

        // Verify that the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Source file not found: {pclPath}");
            return;
        }

        // Load the PCL file into a PDF document using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();
        using (Document pdfDocument = new Document(pclPath, pclLoadOptions))
        {
            // Convert the PDF document to PDF/X‑3 format.
            // ConvertErrorAction.Delete tells the converter to skip objects it cannot convert.
            bool conversionResult = pdfDocument.Convert(
                conversionLog,               // Path to log file for conversion messages
                PdfFormat.PDF_X_3,           // Target PDF/X format
                ConvertErrorAction.Delete    // Action for objects that cannot be converted
            );

            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion to PDF/X failed. See log for details.");
                // Continue to attempt saving; the document may still be partially valid.
            }

            // Save the resulting PDF/X document.
            pdfDocument.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X file saved to '{pdfxPath}'.");
    }
}