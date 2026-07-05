using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files.
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create load options that disable font embedding.
        // HtmlLoadOptions is a concrete LoadOptions class that provides the IsEmbedFonts flag.
        // Setting it to false tells the loader not to embed fonts when converting the source.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions {
            IsEmbedFonts = false
        };

        // Load the PDF document with the custom load options.
        using (Document pdfDocument = new Document(inputPdfPath, loadOptions))
        {
            // Save the document back to PDF. No special save options are required.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved without embedding fonts to '{outputPdfPath}'.");
    }
}