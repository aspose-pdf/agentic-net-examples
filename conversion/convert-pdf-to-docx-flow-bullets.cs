using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX save options as required
            var docOptions = new DocSaveOptions();
            // Set the recognition mode to Flow
            docOptions.Mode = DocSaveOptions.RecognitionMode.Flow;
            // Enable bullet list detection
            docOptions.RecognizeBullets = true;
            // The target format (DOCX) is inferred from the file extension passed to Save.

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}
