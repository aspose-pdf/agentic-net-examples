using System;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Directory where the comparison images will be saved
        const string imagesDir = "DiffImages";

        // Prefix for the generated image files
        const string filePrefix = "diff_";

        // Path of the final ZIP archive containing all diff images
        const string zipPath = "DiffImages.zip";

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

        // Ensure the output directory exists (clean if it already does)
        if (Directory.Exists(imagesDir))
            Directory.Delete(imagesDir, true);
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the two PDF documents (lifecycle: using -> dispose)
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create the comparer and optionally adjust its settings
                GraphicalPdfComparer comparer = new GraphicalPdfComparer
                {
                    // Example: change the highlight color to blue
                    // Color = System.Drawing.Color.Blue,
                    // Example: increase resolution to 300 DPI
                    // Resolution = 300
                };

                // Perform the graphical comparison and output images
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    imagesDir,
                    filePrefix,
                    ImageFormat.Png);
            }

            // Create a ZIP archive containing all generated images
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(imagesDir, zipPath);

            Console.WriteLine($"Comparison images saved to '{imagesDir}'.");
            Console.WriteLine($"ZIP archive created at '{zipPath}'.");
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