using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Image files to be added – adjust the list as needed
        string[] imagePaths = new string[] { "image1.jpg", "image2.png", "image3.bmp" };

        using (Document document = new Document(inputPath))
        {
            // Ensure at least one page exists
            if (document.Pages.Count == 0)
            {
                document.Pages.Add();
            }

            Page page = document.Pages[1];

            // Define the rectangle where each image will be placed (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100.0, 500.0, 300.0, 550.0);

            foreach (string imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                {
                    Console.Error.WriteLine($"Image file not found: {imagePath}");
                    continue;
                }

                try
                {
                    // Attempt to add the image to the page
                    page.AddImage(imagePath, rect);
                }
                catch (Exception ex)
                {
                    // Log the problematic image path and the exception message
                    Console.Error.WriteLine($"Failed to add image '{imagePath}': {ex.Message}");
                }
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
