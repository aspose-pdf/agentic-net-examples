using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf; // DocSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string docxPath = "output.docx";
        const string zipPath = "output_docx.zip";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF and convert to DOCX
            using (Document pdfDoc = new Document(pdfPath))
            {
                var saveOptions = new DocSaveOptions
                {
                    // Export as DOCX
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: enable bullet recognition
                    RecognizeBullets = true
                };

                pdfDoc.Save(docxPath, saveOptions);
            }

            // Verify DOCX was created before compressing
            if (!File.Exists(docxPath))
            {
                Console.Error.WriteLine($"Failed to create DOCX: {docxPath}");
                return;
            }

            // Compress the DOCX into a ZIP archive
            using (FileStream zipToCreate = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                // Add the DOCX file to the archive
                archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath), CompressionLevel.Optimal);
            }

            Console.WriteLine("PDF converted to DOCX and zipped successfully:");
            Console.WriteLine($"  DOCX: {docxPath}");
            Console.WriteLine($"  ZIP : {zipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
