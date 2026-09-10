using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";          // source PDF
        const string docxPath = "output.docx";        // intermediate DOCX
        const string zipPath  = "output.zip";         // final ZIP archive

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and convert it to DOCX using explicit DocSaveOptions
        using (Document pdfDocument = new Document(pdfPath))
        {
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Save as DOCX (WordprocessingML)
                Format = DocSaveOptions.DocFormat.DocX,
                // Use the Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: enable bullet recognition
                RecognizeBullets = true
            };

            pdfDocument.Save(docxPath, saveOptions);
        }

        // Verify that the DOCX was created before compressing
        if (!File.Exists(docxPath))
        {
            Console.Error.WriteLine($"DOCX conversion failed: {docxPath}");
            return;
        }

        // Create a ZIP archive containing the DOCX file
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // Add the DOCX file to the archive; the entry name is just the file name
            archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath));
        }

        Console.WriteLine($"PDF converted to DOCX and compressed into: {zipPath}");
    }
}