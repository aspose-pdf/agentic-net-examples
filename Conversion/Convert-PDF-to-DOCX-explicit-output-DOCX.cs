using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options explicitly
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional)
                    RecognizeBullets = true
                };

                // Save the document as DOCX using the save options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}