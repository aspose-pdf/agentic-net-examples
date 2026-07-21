using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfDiffToZip
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Directory where image diffs will be saved
        string diffDir = Path.Combine(Path.GetTempPath(), "PdfDiffImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(diffDir);

        // Output ZIP file containing all diff images
        const string zipPath = "PdfDiffImages.zip";

        // Validate input files
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create the comparer and generate diff images (PNG format)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    diffDir,
                    "diff_",
                    ImageFormat.Png);
            }

            // Create a ZIP archive and add all generated images
            using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (string filePath in Directory.GetFiles(diffDir))
                {
                    // The entry name inside the ZIP should be just the file name
                    string entryName = Path.GetFileName(filePath);
                    zip.CreateEntryFromFile(filePath, entryName);
                }
            }

            Console.WriteLine($"Diff images have been zipped to '{zipPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary diff directory
            if (Directory.Exists(diffDir))
            {
                try { Directory.Delete(diffDir, true); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}