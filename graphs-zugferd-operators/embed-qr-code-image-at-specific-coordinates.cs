using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string qrImagePath = "qr.png";
        const string outputPdf = "output.pdf";

        // Desired position (lower‑left corner) and size of the QR code on the page
        const double posX = 100;   // X coordinate in points
        const double posY = 200;   // Y coordinate in points
        const double imgWidth = 150;  // Width in points
        const double imgHeight = 150; // Height in points

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Load the PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (1‑based indexing rule)
            Page page = doc.Pages[1];

            // Add the QR code image to the page resources and obtain its name
            string imageName;
            using (FileStream imgStream = File.OpenRead(qrImagePath))
            {
                imageName = page.Resources.Images.Add(imgStream);
            }

            // Retrieve the XImage object to read its original dimensions
            XImage qrImage = page.Resources.Images[imageName];

            // Compute scaling factors to fit the desired width/height
            double scaleX = imgWidth / qrImage.Width;
            double scaleY = imgHeight / qrImage.Height;

            // Insert low‑level PDF operators to draw the image at the specified location
            page.Contents.Add(new GSave()); // q – Save graphics state
            page.Contents.Add(new ConcatenateMatrix(scaleX, 0, 0, scaleY, posX, posY)); // cm – Position & scale
            page.Contents.Add(new Do(imageName)); // Do – Draw the image
            page.Contents.Add(new GRestore()); // Q – Restore graphics state

            // Save the modified PDF (PDF format, so no special SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"QR code embedded and saved to '{outputPdf}'.");
    }
}
