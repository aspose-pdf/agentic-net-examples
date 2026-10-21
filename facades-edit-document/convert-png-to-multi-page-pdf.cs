using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing the PNG images (ordered alphabetically)
        const string imagesFolder = "Images";
        // Output PDF file
        const string outputPdf = "Combined.pdf";

        // Validate input folder
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get all PNG files and sort them to preserve sequence
        string[] pngFiles = Directory.GetFiles(imagesFolder, "*.png");
        Array.Sort(pngFiles, StringComparer.InvariantCulture);

        if (pngFiles.Length == 0)
        {
            Console.Error.WriteLine("No PNG files found in the specified folder.");
            return;
        }

        // Create a new PDF document
        Document pdfDoc = new Document();

        foreach (string imgPath in pngFiles)
        {
            // Add a new page for each image
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Drawing.Image and bind the PNG file
            Image img = new Image
            {
                File = imgPath,
                // Fit the image to the page size (default margins are respected by using the page dimensions)
                FixWidth = page.PageInfo.Width,
                FixHeight = page.PageInfo.Height
            };

            // Add the image to the page content
            page.Paragraphs.Add(img);
        }

        // Save the combined PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}