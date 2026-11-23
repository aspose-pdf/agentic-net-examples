using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image type

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Keep the image stream open until after the PDF is saved
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // Create a new PDF document inside a using block for deterministic disposal
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // Attach the image stream as a background image
                // The correct API is Page.BackgroundImage, not a non‑existent Background class
                page.BackgroundImage = new Image { ImageStream = imgStream };

                // Save the document as PDF (default format)
                doc.Save(outputPdf);
            } // Document disposed after Save()
        } // Image stream disposed after the document has been saved
    }
}
