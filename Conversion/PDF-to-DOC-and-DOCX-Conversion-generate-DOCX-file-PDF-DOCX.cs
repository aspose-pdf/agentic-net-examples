using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and save options

class PdfToDocxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Desired output DOCX file path
        const string docxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure save options for DOCX output
            var saveOptions = new DocSaveOptions
            {
                // Specify the target format as DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Optional: improve bullet detection and spacing
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOCX file using the explicit save options
            pdfDocument.Save(docxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {docxPath}");
    }
}