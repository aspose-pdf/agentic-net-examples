using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the image once into a byte array so we can reuse the same data for every page
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the image will be placed on each page
            // (XIndent = 50, YIndent = 750, width = 100, height = 50 as an example)
            var rect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);

            // Process the first page separately (optional – you could treat it the same as the others)
            Page firstPage = doc.Pages[1];
            using (var ms = new MemoryStream(imageBytes))
            {
                // Add the image stream to the page's image collection; the method returns the image name
                string imgName = firstPage.Resources.Images.Add(ms);
                // Place the image on the page using the name returned above
                firstPage.AddImage(imgName, rect);
            }

            // Reuse the same image data for all remaining pages
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                using (var ms = new MemoryStream(imageBytes))
                {
                    string imgName = page.Resources.Images.Add(ms);
                    page.AddImage(imgName, rect);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: '{outputPdf}'.");
    }
}
