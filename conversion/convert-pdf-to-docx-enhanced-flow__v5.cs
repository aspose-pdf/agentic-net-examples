using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace only

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX conversion options
                var docOptions = new DocSaveOptions();
                // Set the recognition mode (EnhancedFlow) via the correct property
                docOptions.Mode = DocSaveOptions.RecognitionMode.EnhancedFlow;
                // Adjust horizontal proximity threshold (float literal)
                docOptions.RelativeHorizontalProximity = 0.8F;

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
