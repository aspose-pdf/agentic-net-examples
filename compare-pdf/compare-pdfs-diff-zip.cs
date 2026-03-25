using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string diffDir = "diff_images";
        const string zipPath = "diff_images.zip";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Prepare output directory
        if (Directory.Exists(diffDir))
            Directory.Delete(diffDir, true);
        Directory.CreateDirectory(diffDir);

        try
        {
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(doc1, doc2, diffDir, "diff_", ImageFormat.Png);
            }

            // Zip the generated images
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(diffDir, zipPath);

            Console.WriteLine($"Difference images saved to '{diffDir}' and zipped as '{zipPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}