using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "photo.jpg";
        const string outputPath = "width150.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Load the image via a stream, set its fixed width, and add it to the page
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image
                {
                    ImageStream = fs,
                    FixWidth = 150
                };
                page.Paragraphs.Add(img);

                // Save the PDF while the image stream is still open
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved as '{outputPath}'.");
    }
}
