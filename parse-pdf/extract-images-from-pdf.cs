using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // Path to source PDF
        const string outputFolder = "ExtractedImages";         // Folder to store PNG files

        // ------------------------------------------------------------
        // 1. Ensure a PDF exists – create a minimal PDF with an embedded image
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a simple 100x100 red bitmap in memory
            using (var bmp = new Bitmap(100, 100))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Red);
                }

                using (var imgStream = new MemoryStream())
                {
                    bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgStream.Position = 0;

                    // Build a PDF and embed the image
                    using (var doc = new Document())
                    {
                        Page page = doc.Pages.Add();
                        var pdfImg = new Aspose.Pdf.Image { ImageStream = imgStream };
                        page.Paragraphs.Add(pdfImg);
                        doc.Save(inputPdfPath);
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Ensure the output directory exists
        // ------------------------------------------------------------
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // 3. Load the PDF and extract each image as PNG
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                int imageIndex = 1;

                // Iterate over the image resources of the current page
                foreach (XImage xImg in page.Resources.Images)
                {
                    string outPath = System.IO.Path.Combine(
                        outputFolder,
                        $"page{pageNum}_img{imageIndex}.png");

                    // Save the image as PNG using a FileStream
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
