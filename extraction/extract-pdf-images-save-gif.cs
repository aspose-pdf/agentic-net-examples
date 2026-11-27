using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "TableBorderImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 0;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over all images on the page (XImageCollection is not a dictionary)
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Save the XImage to a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        xImg.Save(imgStream);
                        imgStream.Position = 0; // Reset stream position for reading

                        // Load the image into a System.Drawing.Bitmap
                        using (Bitmap bitmap = new Bitmap(imgStream))
                        {
                            // Construct a unique file name for each image
                            string outPath = Path.Combine(
                                outputFolder,
                                $"image_page{pageNum}_idx{imageIndex}.gif");

                            // Save the bitmap as a GIF file
                            bitmap.Save(outPath, ImageFormat.Gif);
                            Console.WriteLine($"Saved GIF: {outPath}");
                        }
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All table border images have been saved as GIF files.");
    }
}