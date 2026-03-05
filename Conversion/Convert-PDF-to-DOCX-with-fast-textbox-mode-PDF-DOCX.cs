using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options for fast textbox mode
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,               // Output as DOCX
                    Mode   = DocSaveOptions.RecognitionMode.Textbox      // Fast textbox mode
                };

                // Save the document as DOCX
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Converted PDF to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}