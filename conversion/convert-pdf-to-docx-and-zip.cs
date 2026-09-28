using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for DocSaveOptions (in Aspose.Pdf namespace)

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string docxPath  = "output.docx";
        const string zipPath   = "output.zip";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and convert it to DOCX
        using (Document pdfDoc = new Document(pdfPath))
        {
            // SaveOptions for DOCX must be provided explicitly
            DocSaveOptions docSaveOpts = new DocSaveOptions {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(docxPath, docSaveOpts);
        }

        // Verify the DOCX was created before compressing
        if (!File.Exists(docxPath))
        {
            Console.Error.WriteLine($"Failed to create DOCX: {docxPath}");
            return;
        }

        // Compress the DOCX into a ZIP archive
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // Add the DOCX file to the archive; the entry name is just the file name
            archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath));
        }

        Console.WriteLine($"PDF converted to DOCX and compressed to ZIP:\nDOCX: {docxPath}\nZIP:  {zipPath}");
    }
}