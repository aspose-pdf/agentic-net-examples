using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string stampImg  = "logo.png";           // image to use as stamp

        // Percentage offsets (relative to page size)
        const double percentFromLeft   = 10.0; // 10 % of page width
        const double percentFromBottom = 20.0; // 20 % of page height

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the image stamp to each page
            foreach (Page page in doc.Pages)
            {
                // Create the image stamp from a file
                ImageStamp imgStamp = new ImageStamp(stampImg);

                // Compute absolute coordinates from percentages
                double xPos = page.PageInfo.Width  * percentFromLeft   / 100.0;
                double yPos = page.PageInfo.Height * percentFromBottom / 100.0;

                // Position the stamp
                imgStamp.XIndent = xPos; // horizontal coordinate from left
                imgStamp.YIndent = yPos; // vertical coordinate from bottom

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}