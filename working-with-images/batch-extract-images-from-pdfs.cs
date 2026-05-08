using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];
                        int imageIndex = 1;

                        // Iterate over the image collection; it yields XImage objects directly
                        foreach (XImage img in page.Resources.Images)
                        {
                            // Save the image to a memory stream first
                            using (var ms = new MemoryStream())
                            {
                                img.Save(ms); // XImage.Save accepts a Stream, not a file path
                                byte[] imageBytes = ms.ToArray();

                                // Determine a suitable file extension based on the image header
                                string extension = GetImageExtensionFromBytes(imageBytes);

                                // Build a unique file name: <pdfname>_page<page>_img<index>.<ext>
                                string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                                string fileName = $"{pdfName}_page{pageIndex}_img{imageIndex}{extension}";
                                string outPath = Path.Combine(outputFolder, fileName);

                                // Write the image bytes to disk
                                File.WriteAllBytes(outPath, imageBytes);
                                Console.WriteLine($"Saved image: {outPath}");
                            }

                            imageIndex++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Helper method to obtain a file extension based on the image header bytes.
    // Falls back to .png if the format cannot be determined.
    static string GetImageExtensionFromBytes(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 4)
            return ".png"; // default

        // JPEG: FF D8 FF
        if (bytes.Length >= 3 && bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
            return ".jpg";

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (bytes.Length >= 8 && bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return ".png";

        // GIF: 47 49 46 38 (GIF8)
        if (bytes.Length >= 4 && bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x38)
            return ".gif";

        // BMP: 42 4D
        if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
            return ".bmp";

        // TIFF (little endian): 49 49 2A 00
        if (bytes.Length >= 4 && bytes[0] == 0x49 && bytes[1] == 0x49 && bytes[2] == 0x2A && bytes[3] == 0x00)
            return ".tiff";

        // TIFF (big endian): 4D 4D 00 2A
        if (bytes.Length >= 4 && bytes[0] == 0x4D && bytes[1] == 0x4D && bytes[2] == 0x00 && bytes[3] == 0x2A)
            return ".tiff";

        // WebP: RIFF....WEBP
        if (bytes.Length >= 12 && bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
            bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50)
            return ".webp";

        // Default fallback
        return ".png";
    }
}
