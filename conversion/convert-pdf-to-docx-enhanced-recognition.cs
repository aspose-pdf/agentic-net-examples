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
            // Configure DOCX save options with enhanced recognition for tables/graphics
            var saveOptions = new DocSaveOptions();
            // Set the recognition mode (EnhancedFlow) via the 'Mode' property
            saveOptions.Mode = DocSaveOptions.RecognitionMode.EnhancedFlow;
            // Optional settings to improve conversion quality
            saveOptions.RecognizeBullets = true;
            saveOptions.RelativeHorizontalProximity = 2.5f;

            // Save the document as DOCX (format inferred from file extension)
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: {outputDocx}");
    }
}
