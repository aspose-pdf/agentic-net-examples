using System;
using System.IO;
using Aspose.Pdf; // DocSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
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
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the output format as DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use textbox recognition mode
                    Mode = DocSaveOptions.RecognitionMode.Textbox,
                    // Enable bullet list detection
                    RecognizeBullets = true
                };

                // Save the PDF as a DOCX file using the configured options
                pdfDoc.Save(outputDocxPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}