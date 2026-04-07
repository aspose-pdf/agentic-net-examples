using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Select the page to stamp (first page in this example)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Set custom coordinates: XIndent from left, YIndent from bottom
            imgStamp.XIndent = 100; // 100 points from the left edge
            imgStamp.YIndent = 150; // 150 points from the bottom edge

            // Optional: define size and opacity
            imgStamp.Width = 200;   // width in points
            imgStamp.Height = 100;  // height in points
            imgStamp.Opacity = 0.8; // semi‑transparent

            // Add the stamp to the selected page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}