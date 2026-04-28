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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create save options for DOC format; default recognition settings are used
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Explicitly set the output format to .doc
                Format = DocSaveOptions.DocFormat.Doc
            };

            // Save the document as DOC using the save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}
