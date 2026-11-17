using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with AcroForm fields
        const string stampImg  = "stamp.png";      // image to stamp on each page
        const string outputPdf = "output.pdf";     // result PDF (fields preserved)

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create an ImageStamp; configure it as needed (position, opacity, etc.)
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Example positioning – center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Do not flatten the form; keep AcroForm fields editable
                Background = false,
                Opacity    = 0.5
            };

            // Apply the stamp to every page individually (PageCollection has no AddStamp method)
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; AcroForm fields remain intact
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'. AcroForm fields are preserved.");
    }
}