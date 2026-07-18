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

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Load the image once and add it to the resources of the first page.
            // The Add method returns the name under which the image is stored.
            string imageName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imageName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Retrieve the XImage instance by its name for reuse.
            XImage sharedImage = doc.Pages[1].Resources.Images[imageName];

            // Define the rectangle where the image will be placed on each page.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 150, 800);

            // Add the same image to every page.
            foreach (Page page in doc.Pages)
            {
                // Ensure the page's resource collection contains a reference to the shared image.
                if (!page.Resources.Images.Contains(sharedImage))
                {
                    page.Resources.Images.Add(sharedImage);
                }

                // Place the image on the page using the same rectangle.
                // The image data is taken from the shared XImage already present in the resources.
                page.AddImage(sharedImage.ToStream(), rect);
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: '{outputPdf}'.");
    }
}