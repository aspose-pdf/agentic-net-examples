using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "image_manifest.csv";
        const string imageFolder = "extracted_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(imageFolder);

        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,PageNumber,Width,Height");

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Iterate over each page to know the page number of extracted images
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Create a new PdfExtractor for the current page
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the already loaded document
                    extractor.BindPdf(doc);

                    // Restrict extraction to a single page
                    extractor.StartPage = pageNum;
                    extractor.EndPage = pageNum;

                    // Extract images from this page
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Build a unique file name that includes the page number
                        string imageFileName = $"page{pageNum}_img{imageIndex}.png";
                        string imagePath = Path.Combine(imageFolder, imageFileName);

                        // Save the image (default format is PNG when using ImageFormat.Png)
                        extractor.GetNextImage(imagePath, ImageFormat.Png);

                        // Load the saved image to obtain its dimensions (fully qualified to avoid ambiguity)
                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                        {
                            int width = img.Width;
                            int height = img.Height;

                            // Append a line to the CSV manifest
                            csvBuilder.AppendLine($"{imageFileName},{pageNum},{width},{height}");
                        }

                        imageIndex++;
                    }
                }
            }
        }

        // Write the CSV manifest to disk
        File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Extraction complete. Manifest saved to '{outputCsv}'.");
    }
}
