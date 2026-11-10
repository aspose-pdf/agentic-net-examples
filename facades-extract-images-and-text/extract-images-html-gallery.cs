using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesDir = "Images";
        const string htmlPath = "gallery.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(imagesDir);

        // Determine total number of pages in the PDF
        int totalPages = new Document(inputPdf).Pages.Count;
        int imageIndex = 1;

        // Extract images page‑by‑page
        for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);
                // Restrict extraction to the current page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;
                // Optional: extract images defined in resources
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    string imgFileName = $"page{pageNumber}_img{imageIndex}.png";
                    string imgPath = Path.Combine(imagesDir, imgFileName);
                    // Save the image directly to a file in PNG format
                    extractor.GetNextImage(imgPath, ImageFormat.Png);
                    imageIndex++;
                }
            }
        }

        // Build a simple HTML gallery referencing the extracted images
        using (StreamWriter writer = new StreamWriter(htmlPath, false))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"UTF-8\">");
            writer.WriteLine("    <title>PDF Image Gallery</title>");
            writer.WriteLine("    <style>");
            writer.WriteLine("        img { margin:10px; max-width:200px; height:auto; }");
            writer.WriteLine("    </style>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <h1>Extracted Images</h1>");

            foreach (string imgFile in Directory.GetFiles(imagesDir))
            {
                // Use forward slashes for HTML src attribute
                string relativePath = Path.Combine(imagesDir, Path.GetFileName(imgFile)).Replace("\\", "/");
                writer.WriteLine($"    <img src=\"{relativePath}\" alt=\"Extracted image\" />");
            }

            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Images extracted to '{imagesDir}'. HTML gallery created at '{htmlPath}'.");
    }
}
