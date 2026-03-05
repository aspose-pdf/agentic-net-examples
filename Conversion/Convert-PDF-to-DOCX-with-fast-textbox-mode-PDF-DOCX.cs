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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX save options for fast textbox mode
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use the Textbox recognition mode (fast, preserves original look)
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Converted PDF to DOCX: {outputPath}");
    }
}