using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";
        const string compressedZip = "output_pptx.zip";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Convert PDF to PPTX
        using (Document pdfDoc = new Document(inputPdf))
        {
            PptxSaveOptions saveOptions = new PptxSaveOptions();
            pdfDoc.Save(outputPptx, saveOptions);
        }

        // Compress the generated PPTX into a ZIP archive
        using (FileStream zipStream = new FileStream(compressedZip, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(outputPptx, Path.GetFileName(outputPptx), CompressionLevel.Optimal);
        }

        Console.WriteLine($"Converted PDF to PPTX: {outputPptx}");
        Console.WriteLine($"Compressed PPTX into ZIP: {compressedZip}");
    }
}
