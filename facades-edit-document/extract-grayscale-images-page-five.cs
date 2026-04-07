using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages (1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            Page page = doc.Pages[5];
            int imageCounter = 1;

            foreach (XImage xImg in page.Resources.Images)
            {
                // Save the original image as JPEG using a FileStream (XImage.Save expects a Stream)
                string jpegPath = $"image_{imageCounter}.jpg";
                try
                {
                    using (FileStream fs = new FileStream(jpegPath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save image {imageCounter}: {ex.Message}");
                    continue;
                }

                // Convert the saved JPEG to grayscale using System.Drawing
                try
                {
                    // Fully qualify System.Drawing types to avoid ambiguity with Aspose.Pdf types
                    using (System.Drawing.Image original = System.Drawing.Image.FromFile(jpegPath))
                    using (System.Drawing.Bitmap grayBitmap = new System.Drawing.Bitmap(original.Width, original.Height))
                    {
                        for (int y = 0; y < original.Height; y++)
                        {
                            for (int x = 0; x < original.Width; x++)
                            {
                                // original must be cast to Bitmap to use GetPixel
                                System.Drawing.Color pixel = ((System.Drawing.Bitmap)original).GetPixel(x, y);
                                int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                                System.Drawing.Color grayColor = System.Drawing.Color.FromArgb(grayValue, grayValue, grayValue);
                                grayBitmap.SetPixel(x, y, grayColor);
                            }
                        }

                        string grayPath = $"image_{imageCounter}_gray.jpg";
                        grayBitmap.Save(grayPath, ImageFormat.Jpeg);
                        Console.WriteLine($"Grayscale image saved: {grayPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to convert image {imageCounter} to grayscale: {ex.Message}");
                }

                imageCounter++;
            }
        }
    }
}
