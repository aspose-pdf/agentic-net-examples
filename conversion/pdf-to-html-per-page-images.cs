using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "html_output";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);
        string htmlPath = Path.Combine(outputDir, "output.html");

        using (Document pdfDoc = new Document(pdfPath))
        {
            // In recent Aspose.Pdf versions the RasterImagesSavingMode and ImageSavingCallback
            // members were removed. To keep images external we use PartsEmbeddingMode = NoEmbedding.
            // After the conversion we reorganise the generated image files into per‑page subfolders.
            HtmlSaveOptions options = new HtmlSaveOptions
            {
                // Do not embed images/fonts – they will be saved as external files.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                // Keep a single HTML file (images are external).
                SplitIntoPages = false
            };

            try
            {
                pdfDoc.Save(htmlPath, options);
                Console.WriteLine($"HTML saved to {htmlPath}");

                // ------------------------------------------------------------
                // Re‑organise images into per‑page subfolders.
                // Aspose.Pdf creates a folder named "output_files" (derived from the HTML file name).
                // Image files are named like "page_1_image_0.png", "page_2_image_3.png", etc.
                // We move each file into a dedicated "page_{n}" subfolder.
                // ------------------------------------------------------------
                string imagesRootFolder = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(htmlPath) + "_files");
                if (Directory.Exists(imagesRootFolder))
                {
                    foreach (string imageFile in Directory.GetFiles(imagesRootFolder))
                    {
                        string fileName = Path.GetFileName(imageFile);
                        // Expected pattern: page_{pageNumber}_... (e.g., page_1_image_0.png)
                        var match = Regex.Match(fileName, @"page_(\d+)", RegexOptions.IgnoreCase);
                        string pageFolderName = match.Success ? $"page_{match.Groups[1].Value}" : "page_unknown";
                        string pageFolderPath = Path.Combine(outputDir, pageFolderName);
                        Directory.CreateDirectory(pageFolderPath);
                        string destPath = Path.Combine(pageFolderPath, fileName);
                        File.Move(imageFile, destPath, overwrite: true);
                    }
                }
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}
