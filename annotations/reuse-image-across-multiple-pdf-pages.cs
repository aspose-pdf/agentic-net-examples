using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "sharedImage.jpg";

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

        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page to host the shared image resource
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Add the image to the resources of the first page and keep the resource name
            string sharedImageName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                sharedImageName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Reuse the same image on five pages (existing or newly added)
            for (int i = 1; i <= 5; i++)
            {
                // Add new pages if the document has fewer than five pages
                if (i > doc.Pages.Count)
                    doc.Pages.Add();

                Page page = doc.Pages[i];

                // Define a rectangle that covers the whole page (adjust as needed)
                var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Add the shared image to the page using the resource name
                page.AddImage(sharedImageName, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with reused image: {outputPdfPath}");
    }
}
