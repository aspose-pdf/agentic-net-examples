using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace as required

class Program
{
    static void Main()
    {
        // Input JPEG files – adjust the paths as needed
        List<string> jpegFiles = new List<string>
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg"
        };

        const string outputPdf = "combined.pdf";

        // Ensure all input files exist before proceeding
        foreach (string file in jpegFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a new PDF document using the recommended disposal pattern
        using (Document pdfDoc = new Document())
        {
            // For each JPEG, add a new page and place the image on it
            foreach (string imgPath in jpegFiles)
            {
                // Add a blank page to the document
                Page page = pdfDoc.Pages.Add();

                // Create an Image object that references the JPEG file
                Image img = new Image
                {
                    File = imgPath
                };

                // Optionally, set the image rectangle to fit the page.
                // Here we use the full page size (lower‑left 0,0 to upper‑right page width/height).
                img.FixWidth = page.PageInfo.Width;
                img.FixHeight = page.PageInfo.Height;

                // Add the image to the page's content
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}