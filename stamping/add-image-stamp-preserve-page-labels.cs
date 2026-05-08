using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "stamped.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Open the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Create the image stamp ----------
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                Background = false,                         // stamp on top of content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Opacity = 0.5f                              // semi‑transparent
            };

            // ---------- Apply the stamp to every page ----------
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // No need to manipulate page labels – stamping does not affect them.

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added; page labels preserved. Output saved to '{outputPdf}'.");
    }
}
