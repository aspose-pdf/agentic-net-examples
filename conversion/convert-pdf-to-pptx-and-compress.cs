using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string compressedPptxPath = "output_compressed.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to PPTX using PptxSaveOptions (non‑PDF format requires explicit options)
        using (Document pdfDoc = new Document(pdfPath))
        {
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        // Re‑compress the PPTX (PPTX is a ZIP archive) to reduce size
        // Extract each entry from the original PPTX and add it to a new archive with optimal compression
        using (FileStream originalPptxStream = new FileStream(pptxPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive sourceArchive = new ZipArchive(originalPptxStream, ZipArchiveMode.Read, leaveOpen: true))
        using (FileStream compressedPptxStream = new FileStream(compressedPptxPath, FileMode.Create, FileAccess.Write))
        using (ZipArchive destArchive = new ZipArchive(compressedPptxStream, ZipArchiveMode.Create))
        {
            foreach (ZipArchiveEntry entry in sourceArchive.Entries)
            {
                ZipArchiveEntry newEntry = destArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                using (Stream sourceStream = entry.Open())
                using (Stream destStream = newEntry.Open())
                {
                    sourceStream.CopyTo(destStream);
                }
            }
        }

        Console.WriteLine($"PDF converted to PPTX and compressed: {compressedPptxPath}");
    }
}