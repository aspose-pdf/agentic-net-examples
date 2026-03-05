using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For ConvertErrorAction enum (if needed)

class Program
{
    static void Main()
    {
        // Input PCL file path
        const string pclPath = "input.pcl";
        // Output PDF/E-1 file path
        const string pdfEPath = "output_pdfe1.pdf";
        // Optional log file for conversion messages
        const string logPath = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Use a using block to ensure the Document is disposed properly
        using (Document doc = new Document(pclPath, pclLoadOptions))
        {
            // Convert the loaded document to PDF/E-1 format.
            // PdfFormat.PDF_E_1 is the enum value for PDF/E-1 (ISO 24517‑1).
            // ConvertErrorAction.Delete tells the converter to skip objects it cannot convert.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document as a PDF/E-1 file.
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E-1 saved to '{pdfEPath}'.");
    }
}