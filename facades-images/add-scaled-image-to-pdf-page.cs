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
        const string imagePath = "image.jpg";

        // Verify required files exist
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

        // Target page and placement parameters (1‑based page index)
        int pageNumber = 4;          // page to modify
        float x = 50f;               // lower‑left X coordinate
        float y = 700f;              // lower‑left Y coordinate
        float width = 200f;          // scaled width of the image
        float height = 150f;         // scaled height of the image

        // Bind the source PDF, add the image, and save the result
        using (Document doc = new Document(inputPdf))
        {
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);

            // AddImage overload expects an int[] of page numbers and float values for coordinates/sizes
            mend.AddImage(imagePath, new int[] { pageNumber }, x, y, width, height);

            // Save the modified PDF
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image added to page {pageNumber} and saved as '{outputPdf}'.");
    }
}
