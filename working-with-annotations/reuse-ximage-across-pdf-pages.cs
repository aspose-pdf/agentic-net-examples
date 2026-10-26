using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Add the image to the document's XImage collection once.
            // The returned name can be reused on any page.
            string sharedImageName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Adding to the Resources.Images collection of the first page registers the image
                // in the document‑wide XImage collection and returns the internal image name.
                sharedImageName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Iterate over all pages and reuse the same image reference.
            foreach (Page page in doc.Pages)
            {
                // Define where the image should be placed on the current page.
                // Here we position it 10 points from the right and bottom edges.
                double imgWidth = 100;   // desired width in points (adjust as needed)
                double imgHeight = 50;   // desired height in points (adjust as needed)
                double x = page.PageInfo.Width - imgWidth - 10; // 10 pt margin from right
                double y = 10; // 10 pt margin from bottom

                var rect = new Aspose.Pdf.Rectangle(x, y, x + imgWidth, y + imgHeight);

                // Add the previously registered image to the current page.
                page.AddImage(sharedImageName, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with reused image: {outputPdf}");
    }
}
