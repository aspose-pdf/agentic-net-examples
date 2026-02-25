using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocPath = "output.doc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure DOC save options (explicitly required for non‑PDF output)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the target format as classic .doc
                Format = DocSaveOptions.DocFormat.Doc,
                // Use full flow recognition for editable output
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional enhancements
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a Word document using the options
            pdfDocument.Save(outputDocPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocPath}'");
    }
}