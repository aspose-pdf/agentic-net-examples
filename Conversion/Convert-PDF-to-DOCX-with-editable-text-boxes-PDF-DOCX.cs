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
            // Configure save options for DOCX with editable text boxes
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Textbox mode to keep text in editable text boxes
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            // Save the document as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
    }
}