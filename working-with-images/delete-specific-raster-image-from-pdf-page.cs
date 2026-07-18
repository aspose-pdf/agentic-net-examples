using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // not required for this task but safe for any text handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 1;          // 1‑based page index
        const string imageName  = "Im1";      // name of the image to delete (as stored in the PDF)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Verify the image exists in the page resources
            bool imageExists = false;
            foreach (XImage img in page.Resources.Images)
            {
                if (img.Name.Equals(imageName, StringComparison.OrdinalIgnoreCase))
                {
                    imageExists = true;
                    break;
                }
            }

            if (!imageExists)
            {
                Console.Error.WriteLine($"Image \"{imageName}\" not found on page {pageNumber}.");
                return;
            }

            // Delete the image from the collection and remove its reference from page contents.
            // ImageDeleteAction.None removes the reference from the page but keeps the image object.
            page.Resources.Images.Delete(imageName, ImageDeleteAction.None);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image \"{imageName}\" removed. Output saved to \"{outputPath}\".");
    }
}