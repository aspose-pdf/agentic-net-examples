using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options (non‑PDF format requires explicit SaveOptions)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Choose a recognition mode (Flow provides editable text)
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: improve bullet detection
                    RecognizeBullets = true
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}