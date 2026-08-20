using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging; // ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Output ZIP file that will contain the diff images
        const string zipOutputPath = "diff_images.zip";

        // Validate input files
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

        // Create a temporary directory to store the generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "PdfDiffImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Perform graphical comparison and output images to the temp directory
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    tempDir,          // target directory for images
                    "diff_",          // file name prefix
                    ImageFormat.Png   // image format
                );
            }

            // Create the ZIP archive and add all generated images
            using (FileStream zipStream = new FileStream(zipOutputPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                foreach (string imagePath in Directory.GetFiles(tempDir))
                {
                    // Preserve only the file name inside the archive
                    string entryName = Path.GetFileName(imagePath);
                    archive.CreateEntryFromFile(imagePath, entryName);
                }
            }

            Console.WriteLine($"Diff images have been zipped to '{zipOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary directory
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}