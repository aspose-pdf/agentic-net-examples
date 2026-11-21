using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure DOCX save options – use the EnhancedFlow mode which preserves layout,
            // tables and fonts automatically. Additional options such as bullet detection can be enabled.
            var docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,
                Mode   = DocSaveOptions.RecognitionMode.EnhancedFlow, // preserves document flow, tables, fonts
                RecognizeBullets = true                                 // optional: improve list handling
            };

            // Save the PDF as DOCX using the configured options
            pdfDoc.Save(outputDocxPath, docxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
    }
}
