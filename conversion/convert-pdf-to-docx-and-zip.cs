using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for DocSaveOptions (in Aspose.Pdf namespace)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string zipPath        = "output.zip";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Convert PDF to DOCX
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // SaveOptions must be provided for non‑PDF formats
            DocSaveOptions docOptions = new DocSaveOptions {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocxPath, docOptions);
        }

        // Compress the resulting DOCX into a ZIP archive
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // Add the DOCX file to the archive
            archive.CreateEntryFromFile(outputDocxPath, Path.GetFileName(outputDocxPath));
        }

        Console.WriteLine($"PDF converted to DOCX and compressed to ZIP:\nDOCX: {outputDocxPath}\nZIP:  {zipPath}");
    }
}