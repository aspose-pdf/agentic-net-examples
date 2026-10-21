using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Folder containing the PDF files to process (created if missing so the sample runs out‑of‑the‑box)
        string inputFolder = Path.Combine(baseDir, "pdfs");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder not found – created empty folder at '{inputFolder}'. Place PDF files there and re‑run.");
            return; // nothing to process yet
        }

        // Root folder where subfolders with extracted images will be created
        string outputRoot = Path.Combine(baseDir, "extracted_images");
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Iterate over all PDF files in the input folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Create a subfolder named after the PDF (without extension)
                string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                string outDir = Path.Combine(outputRoot, pdfName);
                Directory.CreateDirectory(outDir);

                // Use PdfExtractor (from Aspose.Pdf.Facades) to extract images
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    // Older Aspose.Pdf versions do not expose ExtractImageMode – ExtractImage extracts all images.
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            extractor.GetNextImage(imgStream);
                            imgStream.Position = 0; // reset stream position before reading

                            // Determine file extension based on image header bytes
                            string ext = GetExtensionFromBytes(imgStream.ToArray());

                            // Build the output file path
                            string imgFile = Path.Combine(outDir, $"image_{imageIndex}{ext}");

                            // Write the image stream to disk
                            using (FileStream file = new FileStream(imgFile, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(file);
                            }
                        }
                        imageIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Error] {ex.Message}");
        }
    }

    // Helper method to map image header bytes to file extensions
    static string GetExtensionFromBytes(byte[] data)
    {
        if (data == null || data.Length < 4)
            return ".img";

        // JPEG: FF D8 FF
        if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            return ".jpg";
        // PNG: 89 50 4E 47
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return ".png";
        // GIF: 47 49 46 38
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            return ".gif";
        // BMP: 42 4D
        if (data[0] == 0x42 && data[1] == 0x4D)
            return ".bmp";
        // TIFF (little endian): 49 49 2A 00
        if (data[0] == 0x49 && data[1] == 0x49 && data[2] == 0x2A && data[3] == 0x00)
            return ".tiff";
        // TIFF (big endian): 4D 4D 00 2A
        if (data[0] == 0x4D && data[1] == 0x4D && data[2] == 0x00 && data[3] == 0x2A)
            return ".tiff";

        // Fallback when type cannot be determined
        return ".img";
    }
}
