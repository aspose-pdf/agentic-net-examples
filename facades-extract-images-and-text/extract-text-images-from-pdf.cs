using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Directories are provided via environment variables or defaults
        string inputDir = Environment.GetEnvironmentVariable("INPUT_DIR") ?? "/data/input";
        string outputDir = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? "/data/output";

        // Normalize possible Windows style paths when running on Linux containers
        inputDir = NormalizePath(inputDir);
        outputDir = NormalizePath(outputDir);

        // Ensure both directories exist (input may be empty but must exist to avoid DirectoryNotFoundException)
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Process each PDF file found in the input directory
        foreach (string pdfPath in Directory.EnumerateFiles(inputDir, "*.pdf"))
        {
            try
            {
                ProcessPdf(pdfPath, outputDir);
                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Normalizes a path for the current OS – replaces back‑slashes with forward‑slashes on Linux containers
    private static string NormalizePath(string path)
    {
        if (Path.DirectorySeparatorChar == '/' && path.Contains('\\'))
        {
            return path.Replace('\\', '/');
        }
        return path;
    }

    // Extracts text and images from a single PDF file
    static void ProcessPdf(string pdfPath, string outputRoot)
    {
        // Create a subfolder for this PDF's outputs
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string pdfOutputDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(pdfOutputDir);

        // Use PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // --------------------
            // Extract full text
            // --------------------
            extractor.ExtractText();
            string textFile = Path.Combine(pdfOutputDir, $"{pdfName}.txt");
            extractor.GetText(textFile);

            // --------------------
            // Extract images
            // --------------------
            extractor.ExtractImage();
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a separate file (preserve original format)
                string imageFile = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imageFile);
                imageIndex++;
            }
        }
    }
}
