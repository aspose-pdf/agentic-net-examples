using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "tagged_input.pdf";
        const string reportPath = "validation_report.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Perform PDF/UA validation. The method writes an XML report to the
                // supplied path and returns a boolean indicating compliance.
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_UA_1);

                Console.WriteLine(isCompliant
                    ? $"Document is PDF/UA compliant. Report saved to '{reportPath}'."
                    : $"Document is NOT PDF/UA compliant. Report saved to '{reportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}
