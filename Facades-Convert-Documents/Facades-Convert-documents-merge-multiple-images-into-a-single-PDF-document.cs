using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class MergeImagesToPdf
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: output PDF path followed by one or more image file paths
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: MergeImagesToPdf <output.pdf> <image1> [<image2> ...]");
            return;
        }

        string outputPdfPath = args[0];
        var imagePaths = new List<string>(args);
        imagePaths.RemoveAt(0); // remove the output path from the list

        // Verify that all image files exist before proceeding
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // Create a new blank PDF document
            Document pdfDocument = new Document();

            // Remove the default empty page that Aspose.Pdf adds on creation
            pdfDocument.Pages.Clear();

            // Add each image as a separate page
            foreach (string imgPath in imagePaths)
            {
                // Create a new page
                Page page = pdfDocument.Pages.Add();

                // Load the image using a stream (Aspose.Pdf.Image does not have a string‑path ctor)
                Image image = new Image();
                using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    image.ImageStream = fs;
                    // Optionally scale the image to fit the page size while preserving aspect ratio
                    image.FixWidth = page.PageInfo.Width;
                    image.FixHeight = page.PageInfo.Height;
                    // Add the image to the page's content
                    page.Paragraphs.Add(image);
                }
            }

            // Save the resulting PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Successfully merged {imagePaths.Count} image(s) into '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the PDF: {ex.Message}");
        }
    }
}