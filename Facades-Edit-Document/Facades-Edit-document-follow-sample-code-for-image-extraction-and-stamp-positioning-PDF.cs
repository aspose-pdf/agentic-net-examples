using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagesFolder = "ExtractedImages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Ensure the images folder exists
        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        // Load the PDF document
        Document pdfDoc;
        try
        {
            pdfDoc = new Document(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract images from each page and save them as separate files
        // -----------------------------------------------------------------
        int imageCounter = 1;
        foreach (Page page in pdfDoc.Pages)
        {
            // The Images collection contains XImage objects directly
            foreach (XImage xImg in page.Resources.Images)
            {
                // Build a unique file name for the extracted image
                string imgPath = Path.Combine(imagesFolder, $"image_{imageCounter}.png");

                // Save the image to the file system
                try
                {
                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save image '{imgPath}': {ex.Message}");
                }

                imageCounter++;
            }
        }

        // -----------------------------------------------------------------
        // 2. Add an image stamp to each page (positioning example)
        // -----------------------------------------------------------------
        // Use the first extracted image as the stamp source, if any image was found
        string firstImagePath = Path.Combine(imagesFolder, "image_1.png");
        if (File.Exists(firstImagePath))
        {
            // Create an ImageStamp from the extracted image file
            ImageStamp imgStamp = new ImageStamp(firstImagePath)
            {
                // Position the stamp 100 points from the left and bottom edges
                XIndent = 100,
                YIndent = 100,
                // Desired size of the stamp
                Width = 200,
                Height = 200,
                // Semi‑transparent stamp
                Opacity = 0.5
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }
        }
        else
        {
            Console.WriteLine("No images were extracted; stamp will not be added.");
        }

        // -----------------------------------------------------------------
        // 3. Save the edited PDF (save rule)
        // -----------------------------------------------------------------
        try
        {
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
        }
    }
}
