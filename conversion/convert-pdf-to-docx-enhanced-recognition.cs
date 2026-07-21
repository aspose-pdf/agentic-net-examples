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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure save options for DOCX conversion
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // EnhancedFlow mode enables advanced table and graphic recognition
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,
                // Optional settings to improve conversion quality
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the document as DOCX using the specified options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}