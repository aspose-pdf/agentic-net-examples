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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure save options for DOCX conversion
            var saveOptions = new DocSaveOptions
            {
                // Specify the output format as DOCX. Layout preservation is performed automatically.
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document as DOCX with the specified options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}