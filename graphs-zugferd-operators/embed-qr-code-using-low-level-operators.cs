using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string qrImage   = "qr.png";         // QR code image file (PNG, JPG, etc.)
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(qrImage))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImage}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Target page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the QR code will appear
            // left, bottom, width, height
            double left   = 100;   // X coordinate
            double bottom = 500;   // Y coordinate
            double width  = 150;   // desired width
            double height = 150;   // desired height

            // Add the QR code image to the page resources and obtain its name
            string imageName;
            using (FileStream imgStream = File.OpenRead(qrImage))
            {
                imageName = page.Resources.Images.Add(imgStream);
            }

            // Insert low‑level PDF operators using the correct Aspose.Pdf.Operators classes
            // GSave  – save graphics state ("q")
            // ConcatenateMatrix – translate & scale ("cm")
            // Do – draw the image XObject
            // GRestore – restore graphics state ("Q")
            page.Contents.Insert(0, new GSave());
            page.Contents.Insert(1, new ConcatenateMatrix(width, 0, 0, height, left, bottom));
            page.Contents.Insert(2, new Do(imageName));
            page.Contents.Insert(3, new GRestore());

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"QR code embedded and saved to '{outputPdf}'.");
    }
}
