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

        // Directory where comparison images will be saved
        const string imagesDir = "ComparisonImages";

        // Prefix for generated image files
        const string imagePrefix = "diff_";

        // Output ZIP archive containing all images
        const string zipPath = "ComparisonResults.zip";

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

        // Ensure the target directory exists (clean if already present)
        if (Directory.Exists(imagesDir))
            Directory.Delete(imagesDir, true);
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the two PDF documents using the recommended lifecycle pattern
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create the comparer and generate image differences
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    imagesDir,
                    imagePrefix,
                    ImageFormat.Png);
            }

            // Create a ZIP archive from the generated images
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(imagesDir, zipPath);

            Console.WriteLine($"Comparison images zipped to '{zipPath}'.");
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}