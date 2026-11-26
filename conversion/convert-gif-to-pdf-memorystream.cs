using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string gifPath = "image.gif";
        const string pdfPath = "output.pdf";

        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"GIF file not found: {gifPath}");
            return;
        }

        // Load the GIF file into a byte array (in-memory)
        byte[] gifBytes = File.ReadAllBytes(gifPath);

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Load the GIF from a MemoryStream and add it to the page
            using (MemoryStream ms = new MemoryStream(gifBytes))
            {
                // Create an Image object that reads from the stream
                Image img = new Image
                {
                    ImageStream = ms,
                    // Optional: set width (height will preserve aspect ratio)
                    FixWidth = 500,
                    FixHeight = 0
                };

                // Insert the image into the page content
                page.Paragraphs.Add(img);
            }

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"GIF successfully converted to PDF: {pdfPath}");
    }
}