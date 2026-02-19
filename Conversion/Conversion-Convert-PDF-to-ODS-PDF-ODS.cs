using System;
using System.IO;
using Aspose.Pdf;

class PdfToOdsConverter
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPdfPath = "input.pdf";
        string outputOdsPath = "output.ods";

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

            // Configure Excel save options to produce ODS format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Use the nested enum ExcelSaveOptions.ExcelFormat
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the PDF as ODS
            pdfDocument.Save(outputOdsPath, saveOptions);

            Console.WriteLine($"Conversion successful. ODS file saved to '{outputOdsPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
