using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

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
                // Configure DOCX save options (must pass a SaveOptions subclass)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Choose a recognition mode (optional, Flow gives editable text)
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the document as DOCX using the options
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}