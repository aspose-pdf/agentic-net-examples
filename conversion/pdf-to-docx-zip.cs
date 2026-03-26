using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string outputZip = "output.zip";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Convert PDF to DOCX
        using (Document pdfDoc = new Document(inputPdf))
        {
            var docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocx, docxOptions);
        }

        // Compress the resulting DOCX into a ZIP archive
        using (FileStream zipStream = new FileStream(outputZip, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            archive.CreateEntryFromFile(outputDocx, Path.GetFileName(outputDocx));
        }

        Console.WriteLine($"PDF converted to DOCX and zipped: {outputZip}");
    }
}