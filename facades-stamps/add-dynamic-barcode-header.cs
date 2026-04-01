using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Generate a simple barcode image for the current page number
                using (MemoryStream barcodeStream = GenerateBarcodeImage(pageIndex))
                {
                    // Create an image stamp from the barcode stream
                    ImageStamp stamp = new ImageStamp(barcodeStream);
                    stamp.Background = false;               // place over existing content
                    stamp.Opacity = 0.8f;
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment = VerticalAlignment.Top;
                    // Position the stamp a little below the top edge (e.g., 20 points)
                    stamp.YIndent = 20;   // distance from the bottom of the page (since Top alignment is used)
                    stamp.XIndent = 0;    // left offset – not needed when centered, but kept for clarity

                    // Add the stamp to the current page
                    Page page = doc.Pages[pageIndex];
                    page.AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header barcode added to each page. Saved as '{outputPath}'.");
    }

    // Generates a simple barcode‑like bitmap for a given page number and returns it as a MemoryStream
    private static MemoryStream GenerateBarcodeImage(int pageNumber)
    {
        // Define image size (width x height) in pixels
        const int imageWidth = 200;
        const int imageHeight = 50;
        Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // Fill background with white
            graphics.Clear(System.Drawing.Color.White);

            // Simple barcode: draw a black bar for each digit of the page number
            string numberString = pageNumber.ToString();
            int barWidth = imageWidth / (numberString.Length * 2);
            int xPos = 10;
            foreach (char digitChar in numberString)
            {
                int digit = digitChar - '0';
                // Height of the bar varies with the digit value
                int barHeight = 10 + digit * 3;
                System.Drawing.Rectangle barRect = new System.Drawing.Rectangle(
                    xPos,
                    imageHeight - barHeight - 10,
                    barWidth,
                    barHeight);
                using (Brush brush = new SolidBrush(System.Drawing.Color.Black))
                {
                    graphics.FillRectangle(brush, barRect);
                }
                xPos += barWidth * 2; // space between bars
            }
        }

        MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        bitmap.Dispose();
        stream.Position = 0;
        return stream;
    }
}
