using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImage = "stamp.png";
        const string outputPdf = "output.pdf";

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

        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file (Aspose.Pdf.Drawing namespace)
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Rotate the stamp 45 degrees using the correct property
            imgStamp.RotateAngle = 45;

            // Center the stamp on the page using alignment properties
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rotated stamp added and saved to '{outputPdf}'.");
    }
}
