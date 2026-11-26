using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pngPath = "image.png";
        const string pdfPath = "output.pdf";

        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"Input PNG not found: {pngPath}");
            return;
        }

        // Load the PNG image into a memory stream
        using (FileStream fileStream = File.OpenRead(pngPath))
        using (MemoryStream pngStream = new MemoryStream())
        {
            fileStream.CopyTo(pngStream);
            pngStream.Position = 0; // reset for reading

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a new page (pages are 1‑based)
                Page page = pdfDoc.Pages.Add();

                // Set all page margins to zero
                page.PageInfo.Margin = new MarginInfo
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 0,
                    Right = 0
                };

                // Create an Aspose.Pdf.Image paragraph and feed it the stream
                Image img = new Image();
                img.ImageStream = pngStream; // keep the stream open until the document is saved
                // Optionally stretch the image to fill the whole page
                img.FixWidth = page.PageInfo.Width;
                img.FixHeight = page.PageInfo.Height;
                page.Paragraphs.Add(img);

                // Save the PDF
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PNG converted to PDF: {pdfPath}");
    }
}
