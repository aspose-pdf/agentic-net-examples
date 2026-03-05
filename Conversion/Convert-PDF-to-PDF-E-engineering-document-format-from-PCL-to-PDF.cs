using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";          // source PCL file
        const string pdfPath = "output.pdf";         // destination PDF/E file
        const string logPath = "conversion.log";    // conversion log

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Source file not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions loadOptions = new PclLoadOptions();
        // Optional: choose conversion engine
        // loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Wrap Document in a using block for proper disposal
        using (Document doc = new Document(pclPath, loadOptions))
        {
            // Convert to PDF/E (engineered PDF) – PDF/E is based on PDF/X‑3
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/E document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PCL successfully converted to PDF/E: {pdfPath}");
    }
}