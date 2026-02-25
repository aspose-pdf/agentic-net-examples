using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types, including DocSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX output
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX format
                Format = DocSaveOptions.DocFormat.DocX,
                // Optional: use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the explicit save options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}