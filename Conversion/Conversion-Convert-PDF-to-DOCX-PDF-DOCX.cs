using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output DOCX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDocxPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Choose a recognition mode (Flow provides editable output)
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Specify that the target format is DOCX
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocxPath, saveOptions);

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
