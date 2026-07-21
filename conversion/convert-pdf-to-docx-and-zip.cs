using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";   // source PDF
        const string docxPath = "output.docx"; // intermediate DOCX
        const string zipPath  = "output.zip";  // final ZIP archive

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to DOCX ----------
        // Document is disposed automatically via using (lifecycle rule)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // SaveOptions are required for non‑PDF output (save-to-non-pdf rule)
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX format
                Format = DocSaveOptions.DocFormat.DocX,
                // Use full flow recognition for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                RecognizeBullets = true
            };

            pdfDoc.Save(docxPath, saveOptions);
        }

        // Verify DOCX was created
        if (!File.Exists(docxPath))
        {
            Console.Error.WriteLine($"DOCX file was not created: {docxPath}");
            return;
        }

        // ---------- Compress DOCX into a ZIP ----------
        // Remove existing ZIP if present to avoid exceptions
        if (File.Exists(zipPath))
            File.Delete(zipPath);

        // Create a ZIP archive and add the DOCX file
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // The entry name inside the ZIP is just the file name
            archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath));
        }

        Console.WriteLine($"PDF successfully converted to DOCX and compressed to ZIP: {zipPath}");
    }
}