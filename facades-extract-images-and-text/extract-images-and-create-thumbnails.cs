using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "thumbnails";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            int pageNumber = 1;
            foreach (Aspose.Pdf.Page page in pdfDoc.Pages)
            {
                int imageIndex = 1;
                // Iterate over all images on the page
                foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                {
                    // Save the original image to a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        img.Save(imgStream); // stream‑based overload, no ImageSaveOptions needed
                        imgStream.Position = 0;

                        // Load the image with System.Drawing for resizing
                        using (System.Drawing.Image original = System.Drawing.Image.FromStream(imgStream))
                        {
                            // Determine new size while preserving aspect ratio (max dimension = 200)
                            int maxDim = 200;
                            int newWidth, newHeight;
                            if (original.Width > original.Height)
                            {
                                newWidth = maxDim;
                                newHeight = (int)(original.Height * (maxDim / (float)original.Width));
                            }
                            else
                            {
                                newHeight = maxDim;
                                newWidth = (int)(original.Width * (maxDim / (float)original.Height));
                            }

                            // Create the thumbnail bitmap
                            using (Bitmap thumb = new Bitmap(newWidth, newHeight))
                            {
                                using (Graphics g = Graphics.FromImage(thumb))
                                {
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(original, 0, 0, newWidth, newHeight);
                                }

                                // Build output file name: page_image.png
                                string outFile = Path.Combine(
                                    outputFolder,
                                    $"page{pageNumber}_img{imageIndex}.png");

                                // Save as PNG
                                thumb.Save(outFile, ImageFormat.Png);
                                Console.WriteLine($"Thumbnail saved: {outFile}");
                            }
                        }
                    }

                    imageIndex++;
                }

                pageNumber++;
            }
        }

        Console.WriteLine("Image extraction and thumbnail generation completed.");
    }
}
