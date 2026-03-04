using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (Document, SaveOptions)
using Aspose.Pdf.Facades;        // Facade API (PdfFileInfo)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF using a Facade (PdfFileInfo). This also gives access to the underlying Document.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
            {
                Document pdfDoc = pdfInfo.Document; // Retrieve the core Document object.

                // Prepare DOCX save options (must be passed explicitly for non‑PDF output).
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX   // Specify DOCX format.
                };

                // Save the PDF as a DOCX file.
                pdfDoc.Save(outputDocxPath, docxOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}