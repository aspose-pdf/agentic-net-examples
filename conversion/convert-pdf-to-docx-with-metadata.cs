using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Source PDF file
        const string outputDocx = "output.docx"; // Destination DOCX file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set custom metadata properties
            pdfDoc.Info.Author = "John Doe";
            pdfDoc.Info.Title  = "Converted Document";

            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow recognition mode for maximum editability
                Mode   = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition, adjust proximity, etc.
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as DOCX using the specified options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}