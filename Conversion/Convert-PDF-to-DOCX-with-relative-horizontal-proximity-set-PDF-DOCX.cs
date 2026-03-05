using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per task requirement

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output DOCX file path
        const string outputDocx = "output.docx";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOCX conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Set the target format to DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use flow recognition mode for better layout handling
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Adjust horizontal proximity (2.5 means 250% of the font size)
                RelativeHorizontalProximity = 2.5f,
                // Enable bullet recognition during conversion
                RecognizeBullets = true
            };

            // Save the PDF as DOCX using the configured options
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}