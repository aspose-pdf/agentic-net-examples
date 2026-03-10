using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Path to the source PDF
        const string outputFolder = "ExtractedImages";    // Folder to store extracted images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle: create & load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all images defined in the page resources
                int imageIndex = 1;
                foreach (XImage img in page.Resources.Images)
                {
                    // Determine the original image extension (fallback to "png")
                    string extension = GetExtension(img);
                    string fileName = $"page{pageIndex}_img{imageIndex}.{extension}";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the image preserving its original bytes and resolution
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Extracted: {outputPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    /// <summary>
    /// Returns a file extension (without leading dot) for the supplied XImage.
    /// The ImageInfo property is not available in older Aspose.Pdf versions, so we
    /// default to "png" which works for the majority of extracted images.
    /// </summary>
    private static string GetExtension(XImage img)
    {
        // If a future version provides ImageInfo, you could enhance this method.
        // For now, always return a safe default.
        return "png";
    }
}
