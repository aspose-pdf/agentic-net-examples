using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure DOCX save options for Enhanced Flow mode
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX as the target format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // EnhancedFlow provides table recognition and better editability
                    Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                    // Optional enhancements
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as DOCX using the configured options
                pdfDoc.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion succeeded: '{outputDocx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}