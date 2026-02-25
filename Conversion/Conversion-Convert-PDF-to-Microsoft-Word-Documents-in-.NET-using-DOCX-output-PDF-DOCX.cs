using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional)
                    RecognizeBullets = true
                };

                // Save the PDF as DOCX using the specified options
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