using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Output ZIP file containing diff images
        const string zipOutputPath = "diff_images.zip";

        // Verify input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Create a temporary directory for the generated images
        string tempImageDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempImageDir);

        try
        {
            // Load the PDF documents
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Perform graphical comparison and output images
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    tempImageDir,
                    "diff_",
                    ImageFormat.Png);
            }

            // Create ZIP archive and add all generated images
            using (FileStream zipStream = new FileStream(zipOutputPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                foreach (string imagePath in Directory.GetFiles(tempImageDir))
                {
                    string entryName = Path.GetFileName(imagePath);
                    archive.CreateEntryFromFile(imagePath, entryName);
                }
            }

            Console.WriteLine($"Diff images zipped to: {zipOutputPath}");
        }
        finally
        {
            // Clean up temporary image directory
            if (Directory.Exists(tempImageDir))
            {
                try { Directory.Delete(tempImageDir, true); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}