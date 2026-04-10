using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf; // All Aspose.Pdf types (Document, PptxSaveOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF and intermediate PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string compressedPath = "output_compressed.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // ---------- Convert PDF to PPTX ----------
            using (Document pdfDoc = new Document(pdfPath))
            {
                // PptxSaveOptions lives in the Aspose.Pdf namespace
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                pdfDoc.Save(pptxPath, pptxOptions);
            }

            // ---------- Re‑compress the generated PPTX ----------
            // PPTX files are ZIP packages; recompress them with optimal settings
            using (FileStream originalStream = new FileStream(pptxPath, FileMode.Open, FileAccess.Read))
            using (ZipArchive sourceArchive = new ZipArchive(originalStream, ZipArchiveMode.Read, leaveOpen: true))
            using (FileStream compressedStream = new FileStream(compressedPath, FileMode.Create, FileAccess.Write))
            using (ZipArchive targetArchive = new ZipArchive(compressedStream, ZipArchiveMode.Create))
            {
                foreach (ZipArchiveEntry entry in sourceArchive.Entries)
                {
                    // Preserve directory structure; copy each entry with optimal compression
                    ZipArchiveEntry newEntry = targetArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                    using (Stream sourceEntryStream = entry.Open())
                    using (Stream targetEntryStream = newEntry.Open())
                    {
                        sourceEntryStream.CopyTo(targetEntryStream);
                    }
                }
            }

            Console.WriteLine($"Conversion complete. Compressed PPTX saved to '{compressedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
