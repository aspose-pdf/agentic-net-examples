using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process.
        const string inputFolder = "InputPdfs";

        // Folder where extracted images will be saved.
        const string outputFolder = "ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output root folder exists.
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive).
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Create a sub‑folder for each PDF to avoid name clashes.
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Pages are 1‑based in Aspose.Pdf.
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    int imageIndex = 1;

                    // Iterate over the XImage collection (not a dictionary).
                    foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                    {
                        // Build the output file path.
                        string imageFile = Path.Combine(pdfOutputDir,
                            $"page{pageIndex}_img{imageIndex}.png");

                        // Save each image using a FileStream because XImage.Save expects a Stream.
                        using (FileStream fs = new FileStream(imageFile, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs, ImageFormat.Png);
                        }

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"Extracted images from '{pdfPath}' to '{pdfOutputDir}'.");
        }
    }
}
