using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 1;

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Each page has a Resources.Images collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Determine a suitable file extension based on the image format
                    string ext = GetImageExtension(img);

                    // Build a unique file name per image
                    string fileName = $"image_page{page.Number}_{imageIndex}{ext}";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the image preserving its original format using a FileStream
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outPath}");
                    imageIndex++;
                }
            }
        }
    }

    // Helper to map the XImage.ImageFormat (if present) to a file extension.
    // Uses reflection to stay safe if the property is not available in a particular version.
    static string GetImageExtension(XImage img)
    {
        try
        {
            var prop = typeof(XImage).GetProperty("ImageFormat");
            if (prop != null)
            {
                var formatObj = prop.GetValue(img);
                if (formatObj != null)
                {
                    string fmt = formatObj.ToString().ToLowerInvariant();
                    switch (fmt)
                    {
                        case "jpeg":
                        case "jpg":
                            return ".jpg";
                        case "png":
                            return ".png";
                        case "bmp":
                            return ".bmp";
                        case "gif":
                            return ".gif";
                        case "tiff":
                        case "tif":
                            return ".tiff";
                        case "svg":
                            return ".svg";
                        default:
                            return ".bin";
                    }
                }
            }
        }
        catch
        {
            // If reflection fails, fall back to a generic binary extension
        }

        return ".bin";
    }
}
