using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and PptxSaveOptions

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string outputPptxPath    = "output.pptx";
        const string compressedPptxPath = "output_pptx.zip";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Use the documented conversion pattern with PptxSaveOptions
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize save options for PPTX
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the PDF as a PPTX file
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        // Verify conversion succeeded
        if (!File.Exists(outputPptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX file: {outputPptxPath}");
            return;
        }

        // ---------- Compress the PPTX ----------
        // PPTX files are ZIP packages; we re‑package them with optimal compression
        using (FileStream sourceStream = new FileStream(outputPptxPath, FileMode.Open, FileAccess.Read))
        using (FileStream zipStream    = new FileStream(compressedPptxPath, FileMode.Create, FileAccess.Write))
        using (ZipArchive archive       = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: false))
        {
            // Create a single entry inside the zip that holds the PPTX content
            ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(outputPptxPath), CompressionLevel.Optimal);

            using (Stream entryStream = entry.Open())
            {
                sourceStream.CopyTo(entryStream);
            }
        }

        Console.WriteLine($"PDF converted to PPTX: {outputPptxPath}");
        Console.WriteLine($"Compressed PPTX saved as: {compressedPptxPath}");
    }
}