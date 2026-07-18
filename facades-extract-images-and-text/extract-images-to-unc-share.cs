using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // ---------------------------------------------------------------------
    // RULE: When working with UNC paths, always validate that the share is
    // reachable before attempting to create directories or write files.
    // If the UNC share cannot be accessed, fall back to a local temporary
    // folder so the application does not crash with an IOException.
    // ---------------------------------------------------------------------
    private static string GetWritableFolder(string uncPath)
    {
        // If the UNC path is empty or null, skip validation.
        if (string.IsNullOrWhiteSpace(uncPath))
            return Path.Combine(Path.GetTempPath(), "ExtractedImages");

        try
        {
            // Attempt to create the directory. If the share does not exist an
            // IOException will be thrown. Directory.Exists alone is not enough
            // because it returns false for inaccessible UNC shares without
            // throwing.
            DirectoryInfo di = Directory.CreateDirectory(uncPath);
            // Verify we can write a test file – this guarantees write access.
            string testFile = Path.Combine(di.FullName, "__test.tmp");
            File.WriteAllText(testFile, "test");
            File.Delete(testFile);
            return di.FullName;
        }
        catch (IOException)
        {
            // UNC share not reachable – fall back to a local temp folder.
            string fallback = Path.Combine(Path.GetTempPath(), "ExtractedImages");
            Directory.CreateDirectory(fallback);
            Console.WriteLine($"[Warning] UNC path '{uncPath}' is not accessible. Using fallback folder '{fallback}'.");
            return fallback;
        }
        catch (UnauthorizedAccessException)
        {
            // No write permission – also fall back.
            string fallback = Path.Combine(Path.GetTempPath(), "ExtractedImages");
            Directory.CreateDirectory(fallback);
            Console.WriteLine($"[Warning] No write permission to UNC path '{uncPath}'. Using fallback folder '{fallback}'.");
            return fallback;
        }
    }

    static void Main()
    {
        // Local PDF file to extract images from
        const string inputPdfPath = @"C:\Docs\sample.pdf";

        // UNC network share where extracted images will be saved
        // Example: \\fileserver\shared\images
        const string uncFolder = @"\\fileserver\shared\images";

        // Resolve a folder we can actually write to (UNC or fallback).
        string destinationFolder = GetWritableFolder(uncFolder);

        // Initialize the PDF extractor
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string outputPath = Path.Combine(destinationFolder, $"image-{imageIndex}.jpg");
            extractor.GetNextImage(outputPath);
            imageIndex++;
        }

        Console.WriteLine($"Extraction complete. {imageIndex - 1} image(s) saved to '{destinationFolder}'.");
    }
}
