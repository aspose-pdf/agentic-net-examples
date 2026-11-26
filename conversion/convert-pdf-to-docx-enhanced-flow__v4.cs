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
                // Configure DOCX save options as required
                var saveOptions = new DocSaveOptions();
                // Output format: DOCX
                saveOptions.Format = DocSaveOptions.DocFormat.DocX;
                // Use the enhanced flow recognition mode (property name is "Mode")
                saveOptions.Mode = DocSaveOptions.RecognitionMode.EnhancedFlow;
                // Set the relative horizontal proximity threshold (float literal)
                saveOptions.RelativeHorizontalProximity = 0.3F;

                // Save the PDF as DOCX using the specified options
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
