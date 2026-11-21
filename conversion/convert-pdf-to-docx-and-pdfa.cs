using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string intermediateDocx = "intermediate.docx";
        const string outputPdfA = "output_pdfa.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Step 1: Convert PDF to DOCX using DocSaveOptions
        using (Document pdfDoc = new Document(inputPdf))
        {
            DocSaveOptions docSaveOpts = new DocSaveOptions {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(intermediateDocx, docSaveOpts);
        }

        // Step 2: Load the DOCX and convert it to PDF/A (PDF/A-1b)
        using (Document docxDoc = new Document(intermediateDocx))
        {
            // Convert to PDF/A-1b; log conversion details
            docxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            // Save the PDF/A compliant document (default Save writes PDF)
            docxDoc.Save(outputPdfA);
        }

        Console.WriteLine($"PDF/A file created: {outputPdfA}");
    }
}