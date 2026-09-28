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

        // Convert PDF to PPTX
        using (Document pdfDoc = new Document(pdfPath))
        {
            // PptxSaveOptions resides directly in Aspose.Pdf namespace.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        // Re‑compress the generated PPTX (which is a ZIP package) with optimal compression.
        CompressPptx(pptxPath, compressedPptxPath);

        Console.WriteLine($"Conversion complete. Compressed PPTX saved to '{compressedPptxPath}'.");
    }

    /// <summary>
    /// Reads an existing PPTX file (ZIP package) and writes a new PPTX with optimal compression.
    /// </summary>
    static void CompressPptx(string sourcePath, string destinationPath)
    {
        // Ensure the source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PPTX not found: {sourcePath}");
            return;
        }

        // Create the destination file; overwrite if it already exists.
        using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (ZipArchive sourceArchive = new ZipArchive(sourceStream, ZipArchiveMode.Read, leaveOpen: true))
        using (FileStream destStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
        using (ZipArchive destArchive = new ZipArchive(destStream, ZipArchiveMode.Create))
        {
            foreach (ZipArchiveEntry entry in sourceArchive.Entries)
            {
                // Preserve directory structure and file names.
                ZipArchiveEntry newEntry = destArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                using (Stream entryInput = entry.Open())
                using (Stream entryOutput = newEntry.Open())
                {
                    entryInput.CopyTo(entryOutput);
                }
            }
        }

        // Optionally delete the uncompressed intermediate file.
        try { File.Delete(sourcePath); } catch { /* ignore cleanup errors */ }
    }
}
