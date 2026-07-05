using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

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

        // Load the source PDF (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Add the image once to the XImage collection of the first page.
            // The Add method returns the name of the image resource.
            string imgName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imgName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Define a rectangle where the image will be placed on each page.
            // Fully qualified to avoid ambiguity (rectangle-disambiguation rule).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Reuse the same image on all pages.
            // Page indexing is 1‑based (page-indexing-one-based rule).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Add the image to the page by referencing the existing resource.
                // The AddImage overload that takes a stream will reuse the image
                // already present in the collection (adds a reference, not a duplicate).
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    page.AddImage(imgStream, rect);
                }
            }

            // Save the modified PDF (save-to-non-pdf-always-use-save-options rule not needed here)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: {outputPdf}");
    }
}