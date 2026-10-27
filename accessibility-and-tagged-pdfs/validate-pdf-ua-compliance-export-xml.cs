using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportXml = "ua_report.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            bool isCompliant;

            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Determine whether the document is tagged. In the current Aspose.Pdf API the
                // Document.IsTagged property does not exist; the presence of TaggedContent
                // indicates a tagged PDF.
                if (doc.TaggedContent == null)
                {
                    Console.WriteLine("Warning: The document is not tagged. PDF/UA validation requires a tagged PDF.");
                }

                // Perform PDF/UA validation. The method writes an XML report and returns the
                // compliance result.
                isCompliant = doc.Validate(reportXml, PdfFormat.PDF_UA_1);
            }

            Console.WriteLine(isCompliant
                ? "PDF conforms to PDF/UA."
                : "PDF does NOT conform to PDF/UA. See the XML report for details.");

            // The XML compliance report is written to the specified path
            if (File.Exists(reportXml))
            {
                Console.WriteLine($"Compliance report saved to '{reportXml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
