using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Prepare DOC save options with default recognition settings
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Explicitly set the output format to .doc
                Format = DocSaveOptions.DocFormat.Doc
                // All other properties retain their defaults for basic text extraction
            };

            // Save the document as DOC using the explicit save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}