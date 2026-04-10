using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;
using System.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "portfolio.pdf";
        const string tempFolder = "temp_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure temporary folder exists
        Directory.CreateDirectory(tempFolder);
        var extractedImageFiles = new List<string>();

        // ---------- Extract images from the source PDF ----------
        PdfExtractor extractor = new PdfExtractor();
        try
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG to a temporary file
                string imageFile = System.IO.Path.Combine(tempFolder, $"image_{imageIndex}.png");
                // Use System.Drawing.Imaging.ImageFormat to match the expected overload
                extractor.GetNextImage(imageFile, System.Drawing.Imaging.ImageFormat.Png);
                extractedImageFiles.Add(imageFile);
                imageIndex++;
            }
        }
        finally
        {
            // Release resources held by the extractor
            extractor.Close();
        }

        // ---------- Create a new PDF and add each extracted image as a page ----------
        using (Document portfolioDoc = new Document())
        {
            foreach (string imgPath in extractedImageFiles)
            {
                // Add a new blank page
                Page page = portfolioDoc.Pages.Add();

                // Create an Image object and set its source file (Aspose.Pdf.Drawing.Image)
                Image img = new Image();
                img.File = imgPath;

                // Add the image to the page's paragraphs collection
                page.Paragraphs.Add(img);
            }

            // Save the resulting PDF portfolio
            portfolioDoc.Save(outputPdfPath);
        }

        // ---------- Clean up temporary image files ----------
        foreach (string file in extractedImageFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }

        Console.WriteLine($"Portfolio PDF created at '{outputPdfPath}'.");
    }
}
