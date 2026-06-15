using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Standard recognition mode that preserves layout
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the PDF as DOCX using the specified options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{inputPdf}' → '{outputDocx}'.");
    }
}