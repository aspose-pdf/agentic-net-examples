using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "large_input.pdf";
        const string outputPathPath = "resized_path.pdf";
        const string outputPathStream = "resized_stream.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Measure resizing when binding via file path
        var swPath = Stopwatch.StartNew();
        ResizePdfUsingPath(inputPath, outputPathPath);
        swPath.Stop();
        Console.WriteLine($"Resize using file path: {swPath.ElapsedMilliseconds} ms");

        // Measure resizing when binding via stream
        var swStream = Stopwatch.StartNew();
        ResizePdfUsingStream(inputPath, outputPathStream);
        swStream.Stop();
        Console.WriteLine($"Resize using stream: {swStream.ElapsedMilliseconds} ms");
    }

    // Resizes all pages to A4 (595 x 842 points) using a file path
    static void ResizePdfUsingPath(string sourcePath, string destinationPath)
    {
        // Load the PDF document from the file system
        using (var doc = new Document(sourcePath))
        {
            // Set each page size to A4 dimensions (points)
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = 595;   // A4 width in points
                page.PageInfo.Height = 842;  // A4 height in points
            }

            // Save the modified document
            doc.Save(destinationPath);
        }
    }

    // Resizes all pages to A4 (595 x 842 points) using a stream
    static void ResizePdfUsingStream(string sourcePath, string destinationPath)
    {
        // Open the source PDF as a read‑only stream
        using (FileStream readStream = File.OpenRead(sourcePath))
        {
            // Load the PDF document from the stream
            using (var doc = new Document(readStream))
            {
                // Adjust each page size to A4 dimensions
                foreach (Page page in doc.Pages)
                {
                    page.PageInfo.Width = 595;
                    page.PageInfo.Height = 842;
                }

                // Save the modified document to the destination file
                doc.Save(destinationPath);
            }
        }
    }
}
