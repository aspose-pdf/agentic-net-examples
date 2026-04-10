using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string intermediateDocxPath = "intermediate.docx";
        const string outputPdfAPath = "output_pdfa.pdf";
        const string conversionLog = "conversion.log";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Step 1: Load the source PDF and save it as DOCX
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Optional: use Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                pdfDoc.Save(intermediateDocxPath, docOptions);
                Console.WriteLine($"PDF converted to DOCX: {intermediateDocxPath}");
            }

            // Step 2: Load the generated DOCX and convert it to PDF/A
            using (Document docxDoc = new Document(intermediateDocxPath))
            {
                // Convert to PDF/A-1B, logging any conversion errors
                bool conversionResult = docxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion to PDF/A reported errors. Check the log file.");
                }

                // Save the PDF/A compliant document
                docxDoc.Save(outputPdfAPath);
                Console.WriteLine($"PDF/A document saved: {outputPdfAPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
