using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
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
            // Add the image once to the XImageCollection of the first page.
            // The Add method returns the name (key) of the image in the collection.
            string imgName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imgName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Define the rectangle where the image will be placed on each page.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 150, 800);

            // Place the same image (by name) on every page.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Use the overload that accepts the image name from the collection.
                page.AddImage(imgName, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: {outputPdf}");
    }
}
