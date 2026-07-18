using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_with_image.pdf";
        const string imagePath  = "picture.png";
        const string altText    = "A scenic mountain landscape";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Open the existing PDF document.
            using (Document doc = new Document(inputPdf))
            {
                // Ensure there is at least one page.
                if (doc.Pages.Count == 0)
                    doc.Pages.Add();

                // Add the image to the first page.
                Page page = doc.Pages[1];
                Image img = new Image();
                img.File = imagePath;               // Load image from file.
                page.Paragraphs.Add(img);           // Insert image into page content.

                // Set alternative text for the newly added image.
                // XImage collection is not a dictionary; iterate directly.
                foreach (XImage xImg in page.Resources.Images)
                {
                    // TrySetAlternativeText returns false if the image cannot be uniquely identified.
                    // In this simple scenario it will succeed for the image we just added.
                    bool ok = xImg.TrySetAlternativeText(altText, page);
                    if (!ok)
                        Console.WriteLine("Warning: Could not set alt text for an image on the page.");
                }

                // Save the modified PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with image and alt text: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}