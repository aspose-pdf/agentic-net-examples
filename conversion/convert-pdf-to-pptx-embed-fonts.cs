using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfPath))
        {
            // Enable glyph caching to embed font information in the PPTX
            PptxSaveOptions saveOptions = new PptxSaveOptions();
            saveOptions.CacheGlyphs = true;

            pdfDoc.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with embedded fonts: {pptxPath}");
    }
}