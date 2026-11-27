using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "PDF_Images";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 1;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate directly over the XImage collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Save each image as PNG; XImage.Save expects a Stream, not a file path
                    string filePath = System.IO.Path.Combine(outputFolder, $"image_{imageIndex}.png");
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    Console.WriteLine($"Saved image {imageIndex} to '{filePath}'");
                    imageIndex++;
                }
            }
        }
    }
}
