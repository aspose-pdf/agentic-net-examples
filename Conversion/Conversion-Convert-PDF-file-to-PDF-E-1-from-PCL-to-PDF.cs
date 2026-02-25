using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pclFile   = "input.pcl";          // source PCL file
        const string pdfFile   = "output.pdf";         // destination PDF/E‑1 file
        const string logFile   = "conversion.log";     // optional conversion log

        if (!File.Exists(pclFile))
        {
            Console.Error.WriteLine($"Source file not found: {pclFile}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(pclFile, loadOptions))
        {
            // Convert the document to PDF/E‑1 format.
            // The Convert method writes any conversion errors to the log file.
            // ConvertErrorAction.Delete tells Aspose.Pdf to delete objects it cannot convert.
            doc.Convert(logFile, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document as a PDF/E‑1 file.
            doc.Save(pdfFile);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to '{pdfFile}'.");
    }
}