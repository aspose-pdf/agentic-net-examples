using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths
        const string dataDir = "YOUR_DATA_DIRECTORY";
        string xmlPath = Path.Combine(dataDir, "input.xml");
        string intermediatePdfPath = Path.Combine(dataDir, "intermediate.pdf");
        string pdfxPath = Path.Combine(dataDir, "output_pdfx.pdf");
        string logPath = Path.Combine(dataDir, "conversion_log.txt");

        // Verify XML input exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and convert to PDF
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDocument = new Document(xmlPath, xmlLoadOptions))
        {
            // Save the intermediate PDF
            pdfDocument.Save(intermediatePdfPath);

            // Convert the PDF to PDF/X‑3 format
            bool conversionSucceeded = pdfDocument.Convert(
                logPath,               // log file for conversion messages
                PdfFormat.PDF_X_3,     // target PDF/X format
                ConvertErrorAction.Delete); // action for objects that cannot be converted

            if (!conversionSucceeded)
            {
                Console.Error.WriteLine("Conversion to PDF/X failed. See log for details.");
                return;
            }

            // Save the final PDF/X document
            pdfDocument.Save(pdfxPath);
        }

        Console.WriteLine($"PDF/X file saved to '{pdfxPath}'.");
    }
}