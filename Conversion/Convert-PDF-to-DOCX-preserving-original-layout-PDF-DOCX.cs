using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document and convert it to DOCX while preserving layout
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Textbox mode to retain the original visual layout as much as possible
                Mode = DocSaveOptions.RecognitionMode.Textbox,
                // Optional enhancements
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the document as DOCX
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}