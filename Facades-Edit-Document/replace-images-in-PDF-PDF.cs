using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, replacement image and output PDF paths
        const string inputPdf = "input.pdf";
        const string newImage = "newImage.jpg";
        const string outputPdf = "output.pdf";

        // Page number and image index are 1‑based (Aspose.Pdf uses 1‑based indexing)
        const int pageNumber = 1;   // replace image on the first page
        const int imageIndex = 1;   // replace the first image on that page

        // Validate file existence before proceeding
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfContentEditor facade and bind it to the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Replace the specified image on the given page with the new image file
                editor.ReplaceImage(pageNumber, imageIndex, newImage);

                // Save the modified PDF to the output path
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image replacement completed. Output saved to '{outputPdf}'.");
    }
}