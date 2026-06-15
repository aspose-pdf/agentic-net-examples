using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string compressedPptxPath = "output_compressed.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf
        using (Document pdfDoc = new Document(pdfPath))
        {
            // PptxSaveOptions is the correct way to specify PPTX output
            PptxSaveOptions saveOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, saveOptions);
        }

        // Verify that the PPTX was created
        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX file: {pptxPath}");
            return;
        }

        // Create a temporary folder to extract the PPTX (which is a ZIP archive)
        string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Extract the original PPTX contents
            ZipFile.ExtractToDirectory(pptxPath, tempFolder);

            // Re‑create the PPTX with optimal compression
            // The fourth parameter (false) avoids including the base directory in the archive
            ZipFile.CreateFromDirectory(tempFolder, compressedPptxPath, CompressionLevel.Optimal, false);
        }
        finally
        {
            // Clean up the temporary extraction folder
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }

        // Optionally delete the uncompressed PPTX
        try { File.Delete(pptxPath); } catch { /* ignore */ }

        Console.WriteLine($"Compressed PPTX saved to '{compressedPptxPath}'.");
    }
}