using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output DOCX file path
        const string outputPath = "output.docx";

        // Verify the input file exists
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
                // Configure DOCX save options as required
                var saveOptions = new DocSaveOptions();
                // Set the output format to DOCX (optional – the extension also determines the format)
                saveOptions.Format = DocSaveOptions.DocFormat.DocX;
                // Use textbox recognition mode (correct property is Mode)
                saveOptions.Mode = DocSaveOptions.RecognitionMode.Textbox;
                // Adjust horizontal proximity for textbox detection (float literal)
                saveOptions.RelativeHorizontalProximity = 0.3F;

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
