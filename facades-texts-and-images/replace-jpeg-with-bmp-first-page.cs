using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_replaced.pdf";
        const string bmpPath = "highres.bmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bmpPath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdf);

        // Ensure the PDF has at least one page.
        if (pdfDocument.Pages.Count < 1)
        {
            Console.Error.WriteLine("PDF does not contain any pages.");
            return;
        }

        // Get the first page.
        Page firstPage = pdfDocument.Pages[1];

        // Replace the first image on the page with the high‑resolution BMP.
        if (firstPage.Resources.Images.Count > 0)
        {
            // XImageCollection uses a 1‑based index for Replace.
            using (FileStream bmpStream = File.OpenRead(bmpPath))
            {
                firstPage.Resources.Images.Replace(1, bmpStream);
            }
        }
        else
        {
            Console.Error.WriteLine("No images found on the first page to replace.");
            return;
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPdf);
        Console.WriteLine($"Image replaced and saved to '{outputPdf}'.");
    }
}