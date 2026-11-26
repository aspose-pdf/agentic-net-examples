using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "icon.png";
        const string outputPath = "width500.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a blank page.
            Page page = doc.Pages.Add();

            // Set the page width to 500 points (height remains default).
            page.PageInfo.Width = 500;

            // Load the image via a stream and add it to the page.
            Image img = new Image();
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                img.ImageStream = fs;
                // Optional: fit the image to the page width.
                img.FixWidth = page.PageInfo.Width;
                // Add the image paragraph to the page.
                page.Paragraphs.Add(img);

                // Save the PDF while the stream is still open.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF created and saved as '{outputPath}'.");
    }
}
