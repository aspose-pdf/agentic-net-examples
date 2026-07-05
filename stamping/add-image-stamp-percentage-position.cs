using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string stampImg  = "logo.png";           // image to stamp

        // Percentage offsets (0.0 – 1.0) relative to page size
        const double offsetXPercent = 0.10; // 10 % from the left edge
        const double offsetYPercent = 0.20; // 20 % from the bottom edge

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (using‑statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Iterate over all pages and position the stamp using percentages
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Compute absolute coordinates from percentages
                imgStamp.XIndent = pageWidth  * offsetXPercent;
                imgStamp.YIndent = pageHeight * offsetYPercent;

                // Optionally set size or scaling (here we keep original size)
                // imgStamp.Width  = 100;   // uncomment to set explicit width
                // imgStamp.Height = 50;    // uncomment to set explicit height

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}