using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For annotation types (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with existing annotations
        const string stampImg  = "stamp.png";      // image to use as stamp
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp – you can adjust margins, alignment, opacity, etc.
                // Here we place it at the top‑right corner with a 20‑point margin.
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Top,
                RightMargin         = 20,
                TopMargin           = 20,
                Opacity             = 0.5f,          // semi‑transparent
                // Optional scaling – set Width/Height or Zoom as needed
                // Width = 100,
                // Height = 50
            };

            // Add the stamp to the first page (page indexing is 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);   // existing annotations on the page remain unchanged

            // Save the modified PDF (annotations are preserved automatically)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}