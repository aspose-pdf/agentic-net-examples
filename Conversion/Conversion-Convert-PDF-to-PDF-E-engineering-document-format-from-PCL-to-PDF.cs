using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PCL file and output PDF/E file paths
        const string pclPath = "input.pcl";
        const string pdfEPath = "output.pdf";
        const string logPath = "conversion.log";

        // Verify input file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(pclPath, pclLoadOptions))
        {
            // Convert the document to PDF/E format.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document as a PDF/E file.
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E saved to '{pdfEPath}'. Log written to '{logPath}'.");
    }
}