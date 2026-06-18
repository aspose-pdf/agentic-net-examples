using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set custom metadata properties
            pdfDocument.Info.Author = "John Doe";
            pdfDocument.Info.Title = "Converted Document";

            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format (correct enum value)
                Format = DocSaveOptions.DocFormat.DocX,
                // Enable bullet detection (optional)
                RecognizeBullets = true
                // Note: The 'Mode' property was removed in recent versions and is therefore omitted.
            };

            // Save the PDF as DOCX using the specified options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}
