using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and all SaveOptions subclasses are here

class PdfToDocxConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the desired output format (DOCX)
                Format = DocSaveOptions.DocFormat.DocX,

                // Choose a recognition mode; Flow provides editable text
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the PDF as a DOCX file using the explicit SaveOptions
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}