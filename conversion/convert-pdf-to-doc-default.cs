using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create save options for DOC format.
            // The default settings provide basic text extraction.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify the output as the legacy .doc binary format.
                Format = DocSaveOptions.DocFormat.Doc
                // No additional properties are set; defaults (e.g., Flow mode) are used.
            };

            // Save the document as DOC using the explicit save options.
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}