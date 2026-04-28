using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF
        const string docxPath = "output.docx"; // intermediate DOCX
        const string zipPath = "output.zip";   // final ZIP archive

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Convert PDF to DOCX
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOCX save options
            var saveOptions = new DocSaveOptions
            {
                // Export as DOCX format (correct enum value)
                Format = DocSaveOptions.DocFormat.DocX,
                // Optional: enable bullet recognition
                RecognizeBullets = true
            };

            pdfDocument.Save(docxPath, saveOptions);
        }

        // Verify DOCX was created before compressing
        if (!File.Exists(docxPath))
        {
            Console.Error.WriteLine($"DOCX conversion failed: {docxPath}");
            return;
        }

        // Compress the DOCX into a ZIP archive
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Add the DOCX file to the archive; store it with its file name only
            archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath));
        }

        Console.WriteLine($"PDF converted to DOCX and zipped successfully:\nDOCX: {docxPath}\nZIP: {zipPath}");
    }
}
