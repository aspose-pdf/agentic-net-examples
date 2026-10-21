using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath   = "input.pdf";
        const string imgPath   = "stamp.png";
        const string outputPdf = "output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imgPath))
        {
            Console.Error.WriteLine($"Image not found: {imgPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a memory stream from the image file
            using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imgPath)))
            {
                // Create an ImageStamp from the memory stream (Drawing API)
                ImageStamp stamp = new ImageStamp(imgStream);

                // Scale to 50% of the original dimensions using the Zoom property
                stamp.Zoom = 0.5; // 50% scaling

                // Optional positioning – center on the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the first page of the document
                doc.Pages[1].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
