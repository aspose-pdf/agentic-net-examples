using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageExtractor
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // If the source PDF does not exist, create a minimal placeholder PDF so the sample runs without external files.
        if (!File.Exists(inputPdf))
        {
            CreatePlaceholderPdf(inputPdf);
        }

        // Determine total number of pages using the core Document API
        int totalPages;
        using (Document doc = new Document(inputPdf))
        {
            totalPages = doc.Pages.Count;
        }

        // Use PdfExtractor (Facade) to extract images page‑by‑page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            // Iterate through each page
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Restrict extraction to the current page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Extract images from this page
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each image found on the page
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"Image_Page{pageNumber}_Index{imageIndex}.png");

                    // Save image using the overload that writes the image in its original format.
                    // The original format is preserved; if PNG is required, conversion can be done
                    // afterwards using a separate image‑processing library.
                    extractor.GetNextImage(outputPath);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Helper method that creates a very simple PDF file when the expected input PDF is missing.
    // The PDF contains a single blank page – no images – which allows the extractor to run without throwing.
    private static void CreatePlaceholderPdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a single empty page
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}
