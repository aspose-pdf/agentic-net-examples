using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Intermediate PPTX file produced by Aspose.Pdf
        const string pptxPath = "output.pptx";

        // Final compressed PPTX file (still with .pptx extension)
        const string compressedPptxPath = "output_compressed.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // PptxSaveOptions is the correct way to export to PPTX
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the PDF as PPTX
            pdfDocument.Save(pptxPath, saveOptions);
        }

        // ---------- Re‑compress the PPTX (which is a ZIP archive) ----------
        // Create a new ZIP archive with optimal compression and copy all entries
        // from the original PPTX into it. The result is still a valid .pptx file.
        using (FileStream originalStream = new FileStream(pptxPath, FileMode.Open, FileAccess.Read))
        using (FileStream compressedStream = new FileStream(compressedPptxPath, FileMode.Create, FileAccess.Write))
        using (ZipArchive sourceArchive = new ZipArchive(originalStream, ZipArchiveMode.Read, leaveOpen: true))
        using (ZipArchive destArchive = new ZipArchive(compressedStream, ZipArchiveMode.Create))
        {
            foreach (ZipArchiveEntry sourceEntry in sourceArchive.Entries)
            {
                // Preserve the entry path (folders, etc.)
                ZipArchiveEntry destEntry = destArchive.CreateEntry(sourceEntry.FullName, CompressionLevel.Optimal);

                // Copy the entry data
                using (Stream sourceEntryStream = sourceEntry.Open())
                using (Stream destEntryStream = destEntry.Open())
                {
                    sourceEntryStream.CopyTo(destEntryStream);
                }
            }
        }

        // Optionally delete the intermediate uncompressed PPTX
        try { File.Delete(pptxPath); } catch { /* ignore */ }

        Console.WriteLine($"PDF converted to PPTX and compressed: {compressedPptxPath}");
    }
}