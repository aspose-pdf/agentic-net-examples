using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPdf))
        {
            int imageIndex = 1; // Global counter for naming

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    string outputPath = $"Image_{imageIndex}.png";
                    // XImage.Save expects a Stream, not a file path
                    using (FileStream fs = File.Create(outputPath))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }
                    Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'");
                    imageIndex++;
                }
            }
        }
    }
}
