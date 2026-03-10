using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";      // CGM input (input‑only format)
        const string imagePath = "logo.png";       // Image to be referenced multiple times
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load CGM as PDF (CGM cannot be saved, only loaded)
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Add the image once to the resources of the first page.
            // XImageCollection.Add returns the name of the added image.
            string imgName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imgName = doc.Pages[1].Resources.Images.Add(imgStream);
            }

            // Define a rectangle where the image will be placed.
            // Fully qualified to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Place the same image on every page using the same resource name.
            // The AddImage overload that takes a stream will reuse the existing
            // XImage if the stream points to the same image data.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    page.AddImage(imgStream, rect);
                }
            }

            // Save the resulting PDF. No SaveOptions needed because we output PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with shared image references: {outputPdf}");
    }
}