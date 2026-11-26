using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX conversion options
                var saveOptions = new DocSaveOptions();
                // Output format: DOCX
                saveOptions.Format = DocSaveOptions.DocFormat.DocX;
                // Use textbox recognition mode
                saveOptions.Mode = DocSaveOptions.RecognitionMode.Textbox;
                // Adjust horizontal proximity for textbox detection (float literal)
                saveOptions.RelativeHorizontalProximity = 0.8f;

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
