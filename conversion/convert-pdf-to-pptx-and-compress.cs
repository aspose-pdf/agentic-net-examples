using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // Document, PptxSaveOptions are here

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string pptxPath     = "output.pptx";
        const string compressedPptxPath = "output_compressed.pptx";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Load the PDF document (lifecycle: using ensures disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize PPTX save options (core API, no Facades)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save as PPTX
            pdfDocument.Save(pptxPath, pptxOptions);
        }

        // Verify PPTX was created
        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX: {pptxPath}");
            return;
        }

        // ---------- Compress the PPTX (ZIP recompression) ----------
        // PPTX files are ZIP packages; recompress entries with optimal compression.
        // Create a temporary file for the recompressed archive.
        string tempPath = Path.GetTempFileName();

        try
        {
            // Open the original PPTX as a read‑only zip archive
            using (FileStream originalStream = new FileStream(pptxPath, FileMode.Open, FileAccess.Read))
            using (ZipArchive originalArchive = new ZipArchive(originalStream, ZipArchiveMode.Read, leaveOpen: false))
            // Create a new zip archive for the compressed output
            using (FileStream tempStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            using (ZipArchive compressedArchive = new ZipArchive(tempStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                foreach (ZipArchiveEntry entry in originalArchive.Entries)
                {
                    // Preserve directory structure; copy each entry with optimal compression
                    ZipArchiveEntry newEntry = compressedArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                    using (Stream source = entry.Open())
                    using (Stream destination = newEntry.Open())
                    {
                        source.CopyTo(destination);
                    }
                }
            }

            // Replace the original PPTX with the compressed version
            File.Delete(pptxPath);
            File.Move(tempPath, compressedPptxPath);
            Console.WriteLine($"Conversion and compression completed. File saved to '{compressedPptxPath}'.");
        }
        catch (Exception ex)
        {
            // Cleanup temporary file on error
            if (File.Exists(tempPath))
                File.Delete(tempPath);

            Console.Error.WriteLine($"Error during compression: {ex.Message}");
        }
    }
}