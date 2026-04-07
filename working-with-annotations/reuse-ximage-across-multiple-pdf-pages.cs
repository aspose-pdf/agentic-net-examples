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

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Add the image once to the resources of the first page.
            // The Add method returns the name under which the image is stored.
            string imageName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imageName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Define the rectangle where the image will be placed on each page.
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 150, 800);

            // Reuse the same XImage on every page.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Retrieve the XImage object from the collection by its name.
                XImage sharedImage = doc.Pages[1].Resources.Images[imageName];

                // Add a reference to the shared XImage in the current page's resources.
                // This avoids creating a new XObject and keeps the file size small.
                doc.Pages[pageIndex].Resources.Images.Add(sharedImage);

                // Place the image on the page using the same image stream.
                // The ToStream() method returns the original image data.
                doc.Pages[pageIndex].AddImage(sharedImage.ToStream(), rect);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: '{outputPdf}'.");
    }
}