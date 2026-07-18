using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoImage = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoImage);

            // Align the stamp to the right margin of the page
            logoStamp.HorizontalAlignment = HorizontalAlignment.Right;

            // Optional: set other visual properties
            logoStamp.VerticalAlignment   = VerticalAlignment.Center;
            logoStamp.Opacity             = 0.8f; // semi‑transparent

            // Apply the stamp to each page (or target specific pages as needed)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo stamp added with right alignment. Saved to '{outputPdf}'.");
    }
}