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

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Use a using block to guarantee deterministic disposal of the Document
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        int imageIndex = 1;

                        // XImageCollection is enumerable; iterate directly
                        foreach (XImage img in page.Resources.Images)
                        {
                            // Default to PNG as Aspose.Pdf's XImage does not expose its original format
                            string extension = GetExtension(img);

                            // Build a unique filename: <pdfname>_page<page>_img<index>.<ext>
                            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                            string fileName = $"{baseName}_page{pageNum}_img{imageIndex}{extension}";
                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Save the image to the output folder using a stream
                            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs); // XImage.Save accepts a Stream, not a file path
                            }

                            imageIndex++;
                        }
                    }
                }

                Console.WriteLine($"Extracted images from '{Path.GetFileName(pdfPath)}' to '{outputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Helper that returns a safe default extension for extracted images.
    // Aspose.Pdf's XImage does not expose an ImageFormat property, so we default to PNG.
    private static string GetExtension(XImage img)
    {
        return ".png";
    }
}
