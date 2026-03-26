using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDoc = new Document(inputPdf))
        {
            // Default conversion settings
            EpubSaveOptions saveOptions = new EpubSaveOptions();
            pdfDoc.Save(outputEpub, saveOptions);
        }

        Console.WriteLine($"Converted PDF to EPUB: {outputEpub}");
    }
}