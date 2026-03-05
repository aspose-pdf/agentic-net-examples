using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output DOCX file path
        const string docxPath = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document, process, and save as DOCX
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output format as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the PDF as a DOCX file using the specified options
            pdfDocument.Save(docxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {docxPath}");
    }
}