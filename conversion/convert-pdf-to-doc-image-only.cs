using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocPath = "output.doc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOC save options – all option classes reside in Aspose.Pdf namespace
                var saveOptions = new DocSaveOptions
                {
                    // Ensure the output format is DOC (not DOCX)
                    Format = DocSaveOptions.DocFormat.Doc,

                    // Use a recognition mode that preserves the original layout.
                    // Textbox mode keeps the visual appearance and extracts images as they are.
                    Mode = DocSaveOptions.RecognitionMode.Textbox
                };

                // Save the PDF as a DOC file using the custom options
                pdfDocument.Save(outputDocPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: {outputDocPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}