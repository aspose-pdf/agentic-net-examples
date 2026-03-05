using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions classes, including DocSaveOptions, reside in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";
        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure DOCX conversion options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the target format as DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use the Flow recognition mode for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: enable bullet detection during conversion
                    RecognizeBullets = true
                };

                // Save the PDF as a DOCX file, passing the explicit save options
                pdfDocument.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, conversion, or saving
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}