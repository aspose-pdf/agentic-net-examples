using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output DOCX
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure DOCX save options as required
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use flow recognition mode
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Set the relative horizontal proximity threshold (float literal)
                RelativeHorizontalProximity = 0.3f
            };

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}
