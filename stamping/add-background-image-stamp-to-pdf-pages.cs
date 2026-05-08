using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string stampImg  = "stamp.png";      // image to use as stamp
        const string outputPdf = "output.pdf";     // result PDF

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (lifecycle: create → load → save)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp
            ImageStamp stamp = new ImageStamp(stampImg);

            // Set the stamp to be drawn as background (behind page content)
            stamp.Background = true;

            // Optional: position the stamp (centered on each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);   // Page.AddStamp adds the stamp to the page
            }

            // Save the modified PDF (still PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background stamp: {outputPdf}");
    }
}