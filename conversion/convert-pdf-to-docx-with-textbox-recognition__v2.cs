using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure DOCX save options as required
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Set the output format to DOCX
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Use textbox recognition mode
                    Mode = DocSaveOptions.RecognitionMode.Textbox,

                    // Enable bullet detection
                    RecognizeBullets = true,

                    // Adjust horizontal proximity for element grouping (float literal)
                    RelativeHorizontalProximity = 0.3f
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
