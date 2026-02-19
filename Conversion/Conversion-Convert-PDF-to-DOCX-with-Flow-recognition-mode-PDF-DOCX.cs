using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string inputPdfPath = "input.pdf";

        // Output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure DOCX save options with Flow recognition mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use Flow mode for maximum editability of the resulting DOCX
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Explicitly specify DOCX format (optional; inferred from file extension)
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputDocxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
