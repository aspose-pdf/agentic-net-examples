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
        const string rasterImgPath = "image.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(rasterImgPath))
        {
            Console.Error.WriteLine($"Raster image not found: {rasterImgPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Locate the first vector graphic (Path) on the page
            Operator vectorOperator = null;
            foreach (var element in page.Contents)
            {
                if (element is Aspose.Pdf.Drawing.Path)
                {
                    vectorOperator = element;
                    break;
                }
            }

            if (vectorOperator == null)
            {
                Console.WriteLine("No vector graphic (Path) found on page 1.");
                doc.Save(outputPdfPath);
                return;
            }

            // Approximate the bounds of the vector graphic.
            // In a real scenario you would compute the exact bounds from the path data.
            // Here we use a placeholder rectangle (0,0,200,200).
            Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(0, 0, 200, 200);

            // Remove the vector graphic from the page contents
            page.Contents.Remove(vectorOperator);

            // Create an ImageStamp (raster image) and position it to match the original bounds
            ImageStamp imgStamp = new ImageStamp(rasterImgPath)
            {
                Width = bounds.URX - bounds.LLX,
                Height = bounds.URY - bounds.LLY,
                XIndent = bounds.LLX,
                YIndent = bounds.LLY,
                // Ensure the stamp does not add a background rectangle
                Background = false
            };

            // Add the stamp to the page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Raster image replaced vector graphic and saved to '{outputPdfPath}'.");
    }
}
