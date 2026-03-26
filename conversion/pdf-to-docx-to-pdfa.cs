using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string outputPdfA = "output-pdfa.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Step 1: Load the source PDF and save it as DOCX
            using (Document pdfDoc = new Document(inputPdf))
            {
                var docxOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocx, docxOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocx}");
            }

            // Step 2: Load the generated DOCX and convert it to PDF/A‑1B
            using (Document docxDoc = new Document(outputDocx))
            {
                // Convert to PDF/A‑1B; errors are written to the log file
                docxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                // Save the PDF/A compliant document
                docxDoc.Save(outputPdfA);
                Console.WriteLine($"DOCX converted to PDF/A: {outputPdfA}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}