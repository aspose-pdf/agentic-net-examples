using System;
using System.IO;
using Aspose.Pdf;

namespace ResizePagesExample
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";

            // Load the source PDF document. If the file does not exist, create a simple one on‑the‑fly.
            Document document;
            if (File.Exists(inputPath))
            {
                document = new Document(inputPath);
            }
            else
            {
                // Create a new document with a single blank page so the example can run without an external file.
                document = new Document();
                document.Pages.Add();
                Console.WriteLine($"[Info] '{inputPath}' not found – a temporary PDF with one blank page was created.");
            }

            // Find the maximum width and height among all pages.
            double maxWidth = 0.0;
            double maxHeight = 0.0;
            for (int i = 1; i <= document.Pages.Count; i++)
            {
                Page page = document.Pages[i];
                double w = page.PageInfo.Width;
                double h = page.PageInfo.Height;
                if (w > maxWidth) maxWidth = w;
                if (h > maxHeight) maxHeight = h;
            }

            // Resize every page to the largest dimensions using PageInfo (the recommended API).
            for (int i = 1; i <= document.Pages.Count; i++)
            {
                Page page = document.Pages[i];
                page.PageInfo.Width = maxWidth;
                page.PageInfo.Height = maxHeight;
            }

            // Save the modified document.
            document.Save(outputPath);
            Console.WriteLine($"[Success] PDF saved to '{outputPath}'.");
        }
    }
}
