using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Save as EPUB using the default options; must pass an explicit SaveOptions instance
            pdfDoc.Save(outputEpubPath, new EpubSaveOptions());
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
    }
}