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

        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure DOCX conversion options.
            var saveOptions = new DocSaveOptions();
            // Use the 'Mode' property to specify the recognition mode.
            // Textbox mode preserves the original layout of the PDF.
            saveOptions.Mode = DocSaveOptions.RecognitionMode.Textbox;

            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX: {outputPath}");
    }
}