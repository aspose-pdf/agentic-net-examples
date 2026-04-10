using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string imagesDir = "DiffImages";
        const string zipPath   = "DiffImages.zip";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Ensure the output directory exists and is empty
        if (Directory.Exists(imagesDir))
            Directory.Delete(imagesDir, true);
        Directory.CreateDirectory(imagesDir);

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Compare the documents graphically and output images
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.CompareDocumentsToImages(
                doc1,
                doc2,
                imagesDir,          // target directory for images
                "Diff_",            // file name prefix
                System.Drawing.Imaging.ImageFormat.Png // image format
            );
        }

        // Create a zip archive containing all generated images
        if (File.Exists(zipPath))
            File.Delete(zipPath);
        ZipFile.CreateFromDirectory(imagesDir, zipPath);

        Console.WriteLine($"Comparison images saved to '{imagesDir}' and zipped as '{zipPath}'.");
    }
}