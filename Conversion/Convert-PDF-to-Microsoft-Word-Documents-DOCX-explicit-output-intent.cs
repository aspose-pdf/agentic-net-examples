using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired output DOCX file path
        const string outputDocxPath = "output.docx";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure save options for DOCX conversion (explicit output intent)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve bullet detection and spacing handling
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the document as DOCX (lifecycle: save)
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}