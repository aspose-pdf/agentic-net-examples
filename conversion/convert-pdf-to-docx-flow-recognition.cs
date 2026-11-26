using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DocSaveOptions for DOCX output with the required settings
            var docOptions = new DocSaveOptions
            {
                // Specify the output format as DOCX (property exists in this version)
                Format = DocSaveOptions.DocFormat.DocX,

                // Adjust horizontal proximity threshold (float literal)
                RelativeHorizontalProximity = 0.8F
            };

            // Set the recognition mode via the correct property name
            docOptions.Mode = DocSaveOptions.RecognitionMode.Flow;

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}