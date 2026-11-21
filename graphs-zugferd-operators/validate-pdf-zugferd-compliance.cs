using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string logPath = "zugferd_validation_report.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF to ensure it is a valid PDF document.
        using (Document doc = new Document(pdfPath))
        {
            // ZUGFeRD is based on PDF/A‑3. Use Document.Validate with the PDF/A‑3 format.
            // The method returns a bool indicating whether the document conforms to the format.
            // An XML report is written to the supplied log file.
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_3U);

            if (isValid)
            {
                Console.WriteLine("The PDF conforms to the ZUGFeRD specification.");
            }
            else
            {
                Console.WriteLine("The PDF does NOT conform to the ZUGFeRD specification.");
                Console.WriteLine($"See validation report for details: {logPath}");
            }
        }
    }
}
