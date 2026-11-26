using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "sizedpage.pdf";
        const string imagePath = "sample.jpg";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the image stream once and keep it alive until the document is saved
        FileStream imgStream = null;
        try
        {
            imgStream = File.OpenRead(imagePath);

            using (Document doc = new Document())
            {
                // Add a page and set its size to 600 x 800 points
                Page page = doc.Pages.Add();
                page.SetPageSize(600, 800);

                // Create an Aspose.Pdf.Image using the parameter‑less constructor
                Image img = new Image
                {
                    ImageStream = imgStream,
                    // Optional: scale the image to fill the page
                    FixWidth = page.PageInfo.Width,
                    FixHeight = page.PageInfo.Height
                };

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(img);

                // Save the PDF
                doc.Save(outputPdf);
            }
        }
        finally
        {
            // Ensure the stream is closed even if an exception occurs
            imgStream?.Dispose();
        }

        Console.WriteLine($"PDF created and saved as '{outputPdf}'.");
    }
}
