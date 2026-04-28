using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document. No special LoadOptions are required for PDF,
        // but we can use a FileStream to demonstrate explicit loading.
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document pdfDocument = new Document(fs))
        {
            // Create save options. By specifying a default font name,
            // Aspose.PDF will replace missing fonts with this font instead of embedding them.
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial" // Choose a font that is available on the system
            };

            // Save the document with the custom options.
            pdfDocument.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}