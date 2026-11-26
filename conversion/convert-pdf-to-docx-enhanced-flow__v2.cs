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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX save options: set format to DOCX
            var docOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            // Set the recognition mode using the correct property name
            docOptions.Mode = DocSaveOptions.RecognitionMode.EnhancedFlow;

            // Save the PDF as DOCX using the specified options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}
