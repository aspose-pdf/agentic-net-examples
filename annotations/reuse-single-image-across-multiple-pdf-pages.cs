using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "image.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages.
            while (doc.Pages.Count < 3)
            {
                // Add a blank page (default size A4).
                doc.Pages.Add();
            }

            // Reuse the same image file on the first three pages.
            for (int i = 1; i <= 3; i++) // 1‑based page indexing
            {
                Page page = doc.Pages[i];

                // Add the image stream to the page's resource collection.
                // The Add method returns the image name that can be used with Page.AddImage.
                string imgName;
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    imgName = page.Resources.Images.Add(imgStream);
                }

                // Define a rectangle that covers the whole page (or any desired area).
                var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Place the image on the page.
                page.AddImage(imgName, rect);
            }

            // Save the modified PDF. The using block ensures the document stays alive until Save completes.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}
