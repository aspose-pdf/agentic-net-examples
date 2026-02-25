using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify the source file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure DOCX conversion options.
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX.
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use the Flow recognition mode for editable output.
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Set the relative horizontal proximity as requested.
                    RelativeHorizontalProximity = 2.5f,
                    // Enable bullet recognition.
                    RecognizeBullets = true
                };

                // Save the PDF as DOCX using the configured options.
                pdfDocument.Save(outputDocx, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}