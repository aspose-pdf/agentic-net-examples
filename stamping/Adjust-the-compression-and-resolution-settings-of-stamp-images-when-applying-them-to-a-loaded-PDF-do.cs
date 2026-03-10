using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImage = "stamp.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(stampImage))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Adjust compression quality of the stamp image (0‑100%)
            imgStamp.Quality = 70;

            // Optional size and placement settings
            imgStamp.Width = 120;               // stamp width
            imgStamp.Height = 60;               // stamp height
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment = VerticalAlignment.Bottom;
            imgStamp.Opacity = 0.85;            // semi‑transparent

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Optimize the whole document: compress images and limit resolution
            OptimizationOptions opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 60;   // overall image quality (0‑100%)
            opt.ImageCompressionOptions.MaxResolution = 150; // DPI
            opt.ImageCompressionOptions.ResizeImages = true;

            doc.OptimizeResources(opt);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}