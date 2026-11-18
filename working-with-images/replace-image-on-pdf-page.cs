using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string newImagePath  = "newImage.jpg";
        const string outputPdf     = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Proceed only if the page contains at least one image
                if (page.Resources.Images.Count > 0)
                {
                    // Remove the first existing image (index 1 is the first image)
                    page.Resources.Images.Delete(1);

                    // Add the new image from a file stream
                    using (FileStream imgStream = File.OpenRead(newImagePath))
                    {
                        // The Add method inserts the image and returns its index
                        page.Resources.Images.Add(imgStream);
                    }

                    // If you only need to replace a single image in the whole document,
                    // uncomment the following line to stop after the first replacement:
                    // break;
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image replacement completed. Output saved to '{outputPdf}'.");
    }
}