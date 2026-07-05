using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, remove images, and save
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Delete all images on this page
                page.Resources.Images.Delete();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
    }
}