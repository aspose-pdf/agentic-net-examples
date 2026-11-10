using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class ImageExtractorWithManifest
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        string csvPath = Path.Combine(outputFolder, "manifest.csv");

        // -------------------------------------------------------------------
        // 1. Collect image dimensions from the PDF resources (core API).
        // -------------------------------------------------------------------
        var imageDimensions = new Dictionary<(int page, int index), (double width, double height)>();
        using (Document doc = new Document(inputPdf))
        {
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imgIdx = 0;
                foreach (XImage img in page.Resources.Images)
                {
                    imgIdx++;
                    imageDimensions[(pageNum, imgIdx)] = (img.Width, img.Height);
                }
            }
        }

        // -------------------------------------------------------------------
        // 2. Extract images page‑by‑page using PdfExtractor and write the CSV.
        // -------------------------------------------------------------------
        using (StreamWriter csvWriter = new StreamWriter(csvPath))
        {
            csvWriter.WriteLine("FileName,PageNumber,Width,Height");

            // Load the document once to know the total page count.
            using (Document doc = new Document(inputPdf))
            {
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(inputPdf);
                        // Restrict extraction to the current page.
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;
                        // Ensure we extract images defined in the page resources.
                        extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                        extractor.ExtractImage();

                        int imageIndex = 0;
                        while (extractor.HasNextImage())
                        {
                            imageIndex++;
                            string imageFileName = $"image_page{pageNumber}_{imageIndex}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);

                            // Save the image as PNG. The overload writes the image to disk.
                            extractor.GetNextImage(imagePath, ImageFormat.Png);

                            // Retrieve dimensions (fallback to 0 if not found).
                            double width = 0, height = 0;
                            if (imageDimensions.TryGetValue((pageNumber, imageIndex), out var dims))
                            {
                                width = dims.width;
                                height = dims.height;
                            }

                            csvWriter.WriteLine($"{imageFileName},{pageNumber},{width},{height}");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Extraction complete. Images and manifest saved to '{outputFolder}'.");
    }
}
