using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output DOCX file path
        const string docxPath = "output.docx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOCX save options with bullet recognition enabled
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Choose DOCX format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the specified options
            pdfDocument.Save(docxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {docxPath}");
    }
}