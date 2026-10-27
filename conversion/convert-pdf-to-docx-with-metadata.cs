using System;
using System.IO;
using Aspose.Pdf;

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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set custom metadata properties
            pdfDoc.Info.Author = "John Doe";
            pdfDoc.Info.Title  = "Sample Document";

            // Prepare DOCX save options (required for non‑PDF formats)
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the PDF as DOCX using the explicit save options
            pdfDoc.Save(outputDocxPath, docxOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with metadata saved at '{outputDocxPath}'.");
    }
}