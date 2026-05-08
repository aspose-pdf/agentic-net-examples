using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";                 // source PDF
        const string imagesDirPath  = "ExtractedImages";           // folder for images
        const string csvManifestPath = "image_manifest.csv";       // CSV output

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesDirPath);

        // Open the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(csvManifestPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("FileName,PageNumber,Width,Height");

                // Iterate over each page
                foreach (Page page in doc.Pages)
                {
                    int pageNumber = page.Number; // 1‑based page number

                    int imageIndex = 1; // counter per page

                    // Iterate over all images on the current page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for the extracted image
                        string fileName = $"page{pageNumber}_img{imageIndex}.png";
                        string filePath = Path.Combine(imagesDirPath, fileName);

                        // Save the image to disk.
                        // XImage provides a Save method that accepts a Stream.
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        // Write a line to the CSV manifest
                        csvWriter.WriteLine($"{fileName},{pageNumber},{img.Width},{img.Height}");

                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Image extraction complete. Manifest saved to '{csvManifestPath}'.");
    }
}