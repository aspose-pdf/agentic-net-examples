using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampPath = "stamp.png";
        const string outputPdf = "stamped_output.pdf";

        // Verify that the source PDF and stamp image exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the provided image file.
            ImageStamp stamp = new ImageStamp(stampPath);

            // Optionally configure stamp appearance (position, opacity, etc.).
            // Example: place stamp at the bottom‑right corner of each page.
            // stamp.HorizontalAlignment = HorizontalAlignment.Right;
            // stamp.VerticalAlignment = VerticalAlignment.Bottom;

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp applied to all pages. Output saved to '{outputPdf}'.");
    }
}
