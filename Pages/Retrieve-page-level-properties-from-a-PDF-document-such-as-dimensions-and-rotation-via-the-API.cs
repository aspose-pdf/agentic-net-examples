using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; the using block ensures proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (media box or crop box if defined).
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Calculate width and height in points (1 point = 1/72 inch).
                double width = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                // Retrieve the page rotation.
                Rotation rotation = page.Rotate;

                Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt, Rotation = {rotation}");
            }
        }
    }
}