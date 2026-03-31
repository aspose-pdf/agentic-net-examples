using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            Document doc = pdfInfo.Document;
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved ? $"Language set and saved to '{outputPath}'." : "Failed to save the PDF.");
        }
    }
}