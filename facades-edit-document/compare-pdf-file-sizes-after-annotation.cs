using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";
        const string stampPath    = "stamp.png"; // optional image for stamping

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Original file not found: {originalPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Edit the PDF using the high‑level Aspose.Pdf Document API
        // -----------------------------------------------------------------
        Document pdf = new Document(originalPath);

        // If a stamp image is available, add it to page 1 at (100,500)
        if (File.Exists(stampPath))
        {
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent             = 100,   // distance from the left edge
                YIndent             = 500    // distance from the bottom edge
            };

            // Add the stamp to the first page
            pdf.Pages[1].AddStamp(stamp);
        }
        else
        {
            // No stamp image – the document is still saved to create a new version.
        }

        // Save the edited PDF
        pdf.Save(editedPath);

        // -----------------------------------------------------------------
        // Compare file sizes of the original and edited PDFs
        // -----------------------------------------------------------------
        FileInfo originalInfo = new FileInfo(originalPath);
        FileInfo editedInfo   = new FileInfo(editedPath);

        long originalSize = originalInfo.Length;
        long editedSize   = editedInfo.Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Edited   size: {editedSize} bytes");

        if (editedSize != originalSize)
        {
            Console.WriteLine("File sizes differ – changes have been applied.");
        }
        else
        {
            Console.WriteLine("File sizes are identical – no changes detected.");
        }
    }
}
