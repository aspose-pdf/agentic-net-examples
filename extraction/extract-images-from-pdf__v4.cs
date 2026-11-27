using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // retained for potential future image rendering

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagesFolder = "images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            int imageCounter = 1; // simple sequential naming

            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // XImageCollection is not a dictionary – iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string imagePath = Path.Combine(imagesFolder, $"image_{imageCounter:D4}.jpg");

                    // XImage.Save expects a Stream, not a file path.
                    // Write the image data to a FileStream; the original format is preserved.
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Extracted image {imageCounter} to '{imagePath}'");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
