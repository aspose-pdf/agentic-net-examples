using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input MHT file, intermediate PDF, final PDF/X output and log file paths
        const string mhtPath = "input.mht";
        const string pdfPath = "intermediate.pdf";
        const string pdfxPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the MHT file and save it as a regular PDF.
        // ------------------------------------------------------------
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        using (Document pdfDocument = new Document(mhtPath, mhtLoadOptions))
        {
            // Save the intermediate PDF file.
            pdfDocument.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // Step 2: Convert the intermediate PDF to PDF/X‑3 format.
        // ------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create conversion options for PDF/X‑3.
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

            // Perform the conversion. The method returns a bool indicating success.
            bool success = pdfDocument.Convert(conversionOptions);

            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/X failed. Check the log for details.");
                // Optionally, you could write a log file using conversionOptions if needed.
            }

            // Save the resulting PDF/X‑3 document.
            pdfDocument.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X saved to '{pdfxPath}'.");
    }
}