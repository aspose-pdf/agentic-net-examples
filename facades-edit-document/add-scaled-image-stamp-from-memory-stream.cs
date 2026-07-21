using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf";  // destination PDF
        const string imagePath     = "stamp.png";   // image to be used as stamp

        // Validate files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image into a memory stream
        using (FileStream fs = File.OpenRead(imagePath))
        using (MemoryStream imageStream = new MemoryStream())
        {
            fs.CopyTo(imageStream);
            imageStream.Position = 0; // reset stream position for reading

            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Create an ImageStamp from the memory stream
            ImageStamp stamp = new ImageStamp(imageStream);

            // Scale the stamp to 50% of its original size (double precision)
            stamp.Zoom = 0.5; // uniform scaling factor

            // Position the stamp – XIndent/YIndent are the offsets from the lower‑left corner (points)
            stamp.XIndent = 100; // X = 100 points
            stamp.YIndent = 500; // Y = 500 points

            // Add the stamp to every page of the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
