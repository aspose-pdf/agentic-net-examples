using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions are in this namespace
using Aspose.Pdf;               // DocSaveOptions is also here

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Use DocSaveOptions to explicitly request DOC output.
            // Default options provide basic text extraction with default recognition.
            DocSaveOptions docOptions = new DocSaveOptions();

            // Save the document as DOC using the options.
            pdfDoc.Save(outputPath, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputPath}");
    }
}