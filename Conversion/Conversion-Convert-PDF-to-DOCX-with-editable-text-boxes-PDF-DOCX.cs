using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions, DocSaveOptions.DocFormat, DocSaveOptions.RecognitionMode

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX output.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX format.
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Textbox mode to keep layout with editable text boxes.
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Optional: enable bullet recognition for better editing experience.
                RecognizeBullets = true
            };

            // Save the PDF as a DOCX file using the configured options.
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}