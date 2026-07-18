using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Generate a gradient bitmap in memory (top to bottom blue‑white gradient)
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(600, 800))
            using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bmp))
            using (System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                       new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                       System.Drawing.Color.Blue,
                       System.Drawing.Color.White,
                       System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            using (MemoryStream imgStream = new MemoryStream())
            {
                gfx.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                // Save the bitmap to the memory stream in PNG format
                bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                imgStream.Position = 0; // reset stream position

                // Create a background artifact and assign the gradient image
                BackgroundArtifact bgArtifact = new BackgroundArtifact
                {
                    // Ensure the artifact is placed behind page contents
                    IsBackground = true
                };
                // Set the generated image as the background
                bgArtifact.SetImage(imgStream);

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the PDF
            doc.Save("GradientBackground.pdf");
        }

        Console.WriteLine("PDF with gradient background created successfully.");
    }
}
