using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Prepare save options for DOC format (default recognition settings)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Explicitly set the output format to DOC (optional, defaults to DOC)
                Format = DocSaveOptions.DocFormat.Doc
                // No additional settings are required for default text extraction
            };

            // Save the document as DOC using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}