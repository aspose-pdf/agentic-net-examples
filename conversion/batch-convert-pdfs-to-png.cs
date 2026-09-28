using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToPng
{
    static void Main(string[] args)
    {
        // Input and output root folders (adjust as needed)
        string inputRoot  = args.Length > 0 ? args[0] : @"C:\InputPdfs";
        string outputRoot = args.Length > 1 ? args[1] : @"C:\OutputPngs";

        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputRoot}");
            return;
        }

        // Gather all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(inputRoot, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            // Compute relative path to preserve folder hierarchy
            string relativePath = Path.GetRelativePath(inputRoot, pdfPath);
            string relativeDir  = Path.GetDirectoryName(relativePath) ?? string.Empty;

            // Create corresponding output directory
            string outputDir = Path.Combine(outputRoot, relativeDir);
            Directory.CreateDirectory(outputDir);

            // Load PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Build output PNG file name: originalname_page{index}.png
                    string pngFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{i}.png";
                    string pngPath      = Path.Combine(outputDir, pngFileName);

                    // Render page to PNG using PngDevice. Use Process() instead of the non‑existent Save().
                    var pngDevice = new PngDevice(new Resolution(300)); // adjust resolution as needed
                    // Optional: make background transparent
                    // pngDevice.TransparentBackground = true;

                    using (FileStream outStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
                    {
                        pngDevice.Process(page, outStream);
                    }
                }
            }

            Console.WriteLine($"Converted: {pdfPath}");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
