using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // retained for potential other Facade usage

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImg = "stamp.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a concrete ImageStamp (not the abstract Stamp class)
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Position the stamp at (100,200) – use left/bottom alignment with indents
            imgStamp.HorizontalAlignment = HorizontalAlignment.Left;
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            imgStamp.XIndent = 100f; // distance from the left edge
            imgStamp.YIndent = 200f; // distance from the bottom edge

            // Rotate 45 degrees clockwise
            imgStamp.RotateAngle = 45f;

            // Set opacity to 80%
            imgStamp.Opacity = 0.8f;

            // Place the stamp in the foreground (over existing content)
            imgStamp.Background = false;

            // Apply the stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}
