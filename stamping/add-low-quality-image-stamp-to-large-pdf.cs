using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Annotations;   // For Stamp base class (ImageStamp derives from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "large_input.pdf";
        const string outputPdf = "large_output.pdf";
        const string stampImagePath = "logo.png";   // Path to the image to be used as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Reduce image quality to 10% to improve performance on large PDFs
                Quality = 10,

                // Optional: set stamp position and alignment
                // Here we place the stamp at the bottom‑right corner of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Add a small margin from the page edges
                RightMargin = 20,
                BottomMargin = 20,

                // Make the stamp semi‑transparent (optional)
                Opacity = 0.8f
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle: save)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with low‑quality image stamp: {outputPdf}");
    }
}