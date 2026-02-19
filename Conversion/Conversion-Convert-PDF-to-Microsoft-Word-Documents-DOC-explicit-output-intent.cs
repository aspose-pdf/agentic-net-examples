using System;
using System.IO;
using Aspose.Pdf;

class PdfToWordConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string inputPdfPath = "input.pdf";
        // Desired output Word document path (can be .doc or .docx)
        const string outputDocPath = "output.docx";

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

            // Configure save options for Word conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Flow mode provides maximum editability at the cost of layout changes.
                Mode = DocSaveOptions.RecognitionMode.Flow
                // Optional: force DOC format
                // Format = DocSaveOptions.DocFormat.Doc
            };

            // Save the PDF as a Word document using the configured options
            pdfDocument.Save(outputDocPath, saveOptions);

            Console.WriteLine($"Conversion successful. Word document saved to '{outputDocPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
