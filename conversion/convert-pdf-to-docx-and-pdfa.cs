using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";
        const string intermediateDocx = "intermediate.docx";
        const string outputPdfA     = "output_pdfa.pdf";
        const string conversionLog  = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the source PDF and save it as DOCX using explicit DocSaveOptions.
        using (Document pdfDoc = new Document(inputPdf))
        {
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                // Ensure the output format is DOCX.
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(intermediateDocx, docxOptions);
        }

        // Load the generated DOCX and convert it to a PDF/A compliant PDF.
        using (Document docxDoc = new Document(intermediateDocx))
        {
            // Convert to PDF/A‑1b. The log file records any conversion issues.
            docxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            // Save the resulting PDF/A document.
            docxDoc.Save(outputPdfA);
        }

        Console.WriteLine($"PDF/A file created: {outputPdfA}");
    }
}