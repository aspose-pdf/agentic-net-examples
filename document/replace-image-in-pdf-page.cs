using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string newImagePath   = "newImage.jpg";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Work with the first page (adjust as needed)
                Page page = doc.Pages[1];

                // Check if the page already contains image resources
                if (page.Resources.Images.Count > 0)
                {
                    // XImageCollection uses 1‑based indexing.
                    int imageIndex = 1; // replace the first image

                    // Replace the image resource while preserving its placement on the page
                    using (FileStream imgStream = File.OpenRead(newImagePath))
                    {
                        page.Resources.Images.Replace(imageIndex, imgStream);
                    }
                }
                else
                {
                    // No existing images – add the new image at a default location
                    using (FileStream imgStream = File.OpenRead(newImagePath))
                    {
                        // Define a rectangle where the image will be placed
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);
                        page.AddImage(imgStream, rect);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}