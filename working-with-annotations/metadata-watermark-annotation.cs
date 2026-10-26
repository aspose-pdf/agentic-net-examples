using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Build a simple metadata string to encode in the barcode
            string metaData = $"Title:{doc.Info.Title ?? "N/A"};Author:{doc.Info.Author ?? "N/A"}";

            // -------------------------------------------------
            // Generate a barcode‑like image (placeholder) using System.Drawing.
            // System.Drawing is fully qualified to avoid extra using directives.
            // -------------------------------------------------
            using (var bitmap = new System.Drawing.Bitmap(300, 100))
            {
                using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    graphics.Clear(System.Drawing.Color.White);
                    using (var font = new System.Drawing.Font("Arial", 12))
                    {
                        graphics.DrawString(metaData,
                                            font,
                                            new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                            new System.Drawing.PointF(10, 40));
                    }
                }

                // Save the bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray(); // keep the bytes for reuse

                    // -------------------------------------------------
                    // Add the image as a semi‑transparent watermark to each page using ImageStamp
                    // -------------------------------------------------
                    foreach (Page page in doc.Pages)
                    {
                        // Create a fresh stream for the stamp (ImageStamp consumes the stream)
                        using (var imgStream = new MemoryStream(imageBytes))
                        {
                            ImageStamp stamp = new ImageStamp(imgStream)
                            {
                                // Size of the original bitmap (300x100) – you can scale if needed
                                Width = 300,
                                Height = 100,
                                // Center the stamp on the page
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                // Make it semi‑transparent
                                Opacity = 0.5,
                                // No rotation required (default is 0)
                            };

                            // Add the stamp (watermark) to the current page
                            page.AddStamp(stamp);
                        }
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
