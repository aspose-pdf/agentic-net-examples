using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the image and the output PDF
        const string imagePath = "header.jpg";
        const string outputPdf = "multiPageHeader.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the image file as a stream – the stream must stay open until the document is saved
        using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            // Create a new PDF document and ensure it is disposed properly
            using (Document doc = new Document())
            {
                // Add three blank pages (Pages collection is 1‑based)
                doc.Pages.Add(); // Page 1
                doc.Pages.Add(); // Page 2
                doc.Pages.Add(); // Page 3

                // Insert the header image on the first page
                Page firstPage = doc.Pages[1];

                // Create an Image object using the parameter‑less constructor and assign the stream
                Image headerImage = new Image
                {
                    ImageStream = imgStream,
                    // Optional: size the image to the page width (adjust as needed)
                    FixWidth = firstPage.PageInfo.Width,
                    // Example: keep the original aspect ratio by not setting FixHeight, or set a proportion
                    // FixHeight = 100
                };

                // Add the image to the page's paragraph collection
                firstPage.Paragraphs.Add(headerImage);

                // Save the PDF to the specified file
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF created and saved as '{outputPdf}'.");
    }
}
