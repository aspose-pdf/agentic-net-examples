using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string compressedPptxPath = "output_compressed.pptx";

        // Ensure a source PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(pdfPath))
        {
            using (var placeholder = new Document())
            {
                // Add a simple page with some text so the conversion has content.
                var page = placeholder.Pages.Add();
                var paragraph = new TextFragment("Sample PDF content for PPTX conversion.");
                page.Paragraphs.Add(paragraph);
                placeholder.Save(pdfPath);
            }
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize save options for PPTX format (class lives in Aspose.Pdf namespace)
            var saveOptions = new PptxSaveOptions();

            // Save the document as PPTX
            pdfDoc.Save(pptxPath, saveOptions);
        }

        // ---------- Compress the resulting PPTX ----------
        // PPTX files are ZIP archives; recompress them with optimal settings
        RecompressPptx(pptxPath, compressedPptxPath);

        Console.WriteLine($"Conversion complete. Compressed PPTX saved to '{compressedPptxPath}'.");
    }

    /// <summary>
    /// Re‑compresses a PPTX (ZIP) file using optimal compression.
    /// </summary>
    /// <param name="sourcePath">Path to the original PPTX file.</param>
    /// <param name="destinationPath">Path where the compressed PPTX will be written.</param>
    static void RecompressPptx(string sourcePath, string destinationPath)
    {
        // Ensure the destination file does not already exist
        if (File.Exists(destinationPath))
            File.Delete(destinationPath);

        // Open the source PPTX as a read‑only ZIP archive
        using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        using (ZipArchive sourceArchive = new ZipArchive(sourceStream, ZipArchiveMode.Read))
        // Create a new ZIP archive for the compressed output
        using (FileStream destStream = new FileStream(destinationPath, FileMode.CreateNew, FileAccess.Write))
        using (ZipArchive destArchive = new ZipArchive(destStream, ZipArchiveMode.Create))
        {
            // Copy each entry from the source to the destination with optimal compression
            foreach (ZipArchiveEntry entry in sourceArchive.Entries)
            {
                ZipArchiveEntry newEntry = destArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                using (Stream sourceEntryStream = entry.Open())
                using (Stream destEntryStream = newEntry.Open())
                {
                    sourceEntryStream.CopyTo(destEntryStream);
                }
            }
        }
    }
}
