using System;
using System.IO;
using Aspose.Pdf; // Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX conversion options
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Specify DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the enhanced flow recognition mode
                Mode = DocSaveOptions.RecognitionMode.EnhancedFlow,

                // Enable bullet detection
                RecognizeBullets = true,

                // Adjust horizontal proximity for element grouping (float literal)
                RelativeHorizontalProximity = 0.3f
            };

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}
