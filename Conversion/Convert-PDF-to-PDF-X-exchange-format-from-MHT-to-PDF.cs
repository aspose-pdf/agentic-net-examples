using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input MHT file, intermediate PDF, conversion log, and final PDF/X output paths
        const string mhtPath      = "input.mht";
        const string pdfPath      = "intermediate.pdf";
        const string logPath      = "conversion.log";
        const string pdfXPath     = "output_pdfx3.pdf";

        // Verify the source MHT file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Source file not found: {mhtPath}");
            return;
        }

        // Load the MHT file using MhtLoadOptions and save it as a regular PDF
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();
        using (Document doc = new Document(mhtPath, mhtLoadOptions))
        {
            // Save the intermediate PDF (optional but useful for inspection)
            doc.Save(pdfPath);

            // Convert the document to PDF/X‑3 format.
            // The conversion writes any errors to the specified log file.
            bool conversionResult = doc.Convert(
                outputLogFileName: logPath,
                format: PdfFormat.PDF_X_3,
                action: ConvertErrorAction.Delete);

            // Optionally report conversion success/failure
            Console.WriteLine(conversionResult
                ? "Conversion to PDF/X‑3 succeeded."
                : "Conversion to PDF/X‑3 completed with warnings (see log).");

            // Save the PDF/X‑3 compliant document
            doc.Save(pdfXPath);
        }

        Console.WriteLine($"PDF/X‑3 file saved to '{pdfXPath}'.");
    }
}