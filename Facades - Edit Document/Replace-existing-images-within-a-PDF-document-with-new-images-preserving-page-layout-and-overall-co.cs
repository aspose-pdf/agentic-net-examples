using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newImage = "newImage.jpg";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newImage))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImage}");
            return;
        }

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the facade for content editing
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Determine how many images are on the current page
                    int imageCount = doc.Pages[pageNum].Resources.Images.Count;

                    // Replace each image with the new image file, preserving position and size
                    for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                    {
                        editor.ReplaceImage(pageNum, imgIndex, newImage);
                    }
                }

                // Save the modified PDF; Save writes the bound document to the specified path
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"All images replaced. Output saved to '{outputPdf}'.");
    }
}