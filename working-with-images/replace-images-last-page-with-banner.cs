using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bannerImagePath = "banner.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(bannerImagePath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            int lastPageIndex = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageIndex];

            // Remove all existing images from the page resources
            lastPage.Resources.Images.Clear();
            // Also clear any image paragraphs that might be present
            lastPage.Paragraphs.Clear();

            // Add the banner image to the page resources and obtain its name
            using (FileStream imgStream = File.OpenRead(bannerImagePath))
            {
                string imgName = lastPage.Resources.Images.Add(imgStream);

                // Define a rectangle that spans the full width of the page and height.
                // Use Aspose.Pdf.Rectangle (page geometry) to avoid ambiguity.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, lastPage.PageInfo.Width, lastPage.PageInfo.Height);

                // Place the image on the page using the resource name
                lastPage.AddImage(imgName, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner applied and saved to '{outputPdfPath}'.");
    }
}
