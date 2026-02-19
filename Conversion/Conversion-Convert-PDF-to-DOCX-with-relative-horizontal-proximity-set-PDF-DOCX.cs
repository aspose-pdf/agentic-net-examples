using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path and output DOCX path.
        string inputPdfPath = "input.pdf";
        string outputDocxPath = "output.docx";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Configure DOCX save options.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Choose the recognition mode (Flow provides better editability).
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Set the relative horizontal proximity (default is 1.0 = 100% of font size).
                RelativeHorizontalProximity = 1.0f,

                // Specify that the output format should be DOCX.
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the configured options.
            pdfDocument.Save(outputDocxPath, saveOptions);
            Console.WriteLine($"Conversion completed successfully. DOCX saved to '{outputDocxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
