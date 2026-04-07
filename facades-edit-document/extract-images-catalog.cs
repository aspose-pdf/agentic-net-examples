using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string catalogPdf = "catalog.pdf";
        const string tempDir = "temp_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Prepare temporary folder for extracted images
        if (Directory.Exists(tempDir))
        {
            foreach (string file in Directory.GetFiles(tempDir))
            {
                File.Delete(file);
            }
        }
        else
        {
            Directory.CreateDirectory(tempDir);
        }

        // Extract images from the source PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imagePath = Path.Combine(tempDir, $"image-{imageIndex}.png");
            extractor.GetNextImage(imagePath);
            imageIndex++;
        }

        // Create a new PDF that will serve as the image catalog
        using (Document catalog = new Document())
        {
            // Title page
            Page titlePage = catalog.Pages.Add();
            TextFragment title = new TextFragment("Image Catalog");
            title.TextState.FontSize = 24;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.Position = new Position(0, 0);
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            titlePage.Paragraphs.Add(title);

            // Add a page per extracted image
            string[] imageFiles = Directory.GetFiles(tempDir);
            foreach (string imgFile in imageFiles)
            {
                Page page = catalog.Pages.Add();

                // Thumbnail image
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = imgFile;
                img.FixWidth = 200;
                img.FixHeight = 200;
                page.Paragraphs.Add(img);

                // Caption with file name
                string fileName = Path.GetFileName(imgFile);
                TextFragment caption = new TextFragment(fileName);
                caption.TextState.FontSize = 12;
                caption.TextState.Font = FontRepository.FindFont("Helvetica");
                caption.Margin = new MarginInfo(0, 0, 20, 0);
                page.Paragraphs.Add(caption);
            }

            catalog.Save(catalogPdf);
        }

        // Clean up temporary images
        foreach (string file in Directory.GetFiles(tempDir))
        {
            File.Delete(file);
        }
        Directory.Delete(tempDir);
    }
}
