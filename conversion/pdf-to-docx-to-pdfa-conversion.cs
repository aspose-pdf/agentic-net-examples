using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath  = "output.docx";
        const string outputPdfaPath  = "output_pdfa.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Step 1: Convert PDF to DOCX
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                var docSaveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocxPath, docSaveOptions);
            }

            // Step 2: Load the generated DOCX and convert it to PDF/A
            using (Document docxDoc = new Document(outputDocxPath))
            {
                var pdfAOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                {
                    OptimizeFileSize = true
                };
                docxDoc.Convert(pdfAOptions);
                docxDoc.Save(outputPdfaPath);
            }

            Console.WriteLine("PDF → DOCX → PDF/A conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}