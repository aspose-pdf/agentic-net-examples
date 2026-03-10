using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Bind the document to a PdfConverter facade (facade pattern).
                using (PdfConverter converter = new PdfConverter(pdfDoc))
                {
                    // Configure DOCX save options to preserve fidelity.
                    DocSaveOptions saveOptions = new DocSaveOptions
                    {
                        // Export as DOCX.
                        Format = DocSaveOptions.DocFormat.DocX,
                        // Use Flow mode for better editability while keeping layout.
                        Mode = DocSaveOptions.RecognitionMode.Flow,
                        // Preserve Type3 fonts as text when possible.
                        ConvertType3Fonts = true,
                        // Recognize bullet lists.
                        RecognizeBullets = true
                    };

                    // Save the bound document as DOCX using explicit save options.
                    pdfDoc.Save(outputDocx, saveOptions);
                }
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}