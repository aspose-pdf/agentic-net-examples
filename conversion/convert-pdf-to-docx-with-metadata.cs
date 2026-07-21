using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Set custom metadata
                pdfDoc.Info.Author = "John Doe";
                pdfDoc.Info.Title  = "Converted Document";

                // Prepare DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Choose DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use full flow recognition for better editability
                    Mode   = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: enable bullet recognition
                    RecognizeBullets = true
                };

                // Save as DOCX using explicit save options
                pdfDoc.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}