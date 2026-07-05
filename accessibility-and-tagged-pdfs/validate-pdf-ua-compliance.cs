using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportPath = "ua_report.xml";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Validate the document against PDF/UA (PDF_UA_1) standard.
            // The Validate method writes a detailed XML log to the specified file.
            bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_UA_1);

            // Report the overall compliance result
            Console.WriteLine($"PDF/UA compliance: {isCompliant}");

            // Read and display the XML compliance report (optional)
            if (File.Exists(reportPath))
            {
                string xmlReport = File.ReadAllText(reportPath);
                Console.WriteLine("Compliance report (XML):");
                Console.WriteLine(xmlReport);
            }
        }
    }
}