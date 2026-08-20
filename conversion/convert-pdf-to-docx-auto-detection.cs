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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure save options for DOCX conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use automatic content detection (Flow mode)
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document as DOCX using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
    }
}