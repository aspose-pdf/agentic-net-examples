using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output DOCX
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DocSaveOptions with the required settings
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify that the target format is DOCX
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Use the enhanced flow recognition mode
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                    // Enable bullet detection
                    RecognizeBullets = true,

                    // Adjust horizontal proximity for element grouping (float literal)
                    RelativeHorizontalProximity = 0.8f
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocxPath, docOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
