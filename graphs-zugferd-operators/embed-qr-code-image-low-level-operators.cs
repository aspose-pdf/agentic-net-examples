using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string qrImagePath   = "qr.png";          // QR code image file
        // Desired placement rectangle (lower‑left corner X,Y and size)
        const double posX = 100;   // X coordinate
        const double posY = 500;   // Y coordinate
        const double width = 150;  // image width
        const double height = 150; // image height

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Add the QR image to the page resources and obtain its name
            string imageName;
            using (FileStream imgStream = File.OpenRead(qrImagePath))
            {
                imageName = page.Resources.Images.Add(imgStream);
            }

            // Insert low‑level PDF operators using the proper Aspose.Pdf.Operators classes
            // GSave  -> "q"  (save graphics state)
            // ConcatenateMatrix -> "cm" (set transformation matrix)
            // Do -> "Do" (draw the image XObject)
            // GRestore -> "Q" (restore graphics state)
            page.Contents.Insert(0, new GSave());
            page.Contents.Insert(1, new ConcatenateMatrix(width, 0, 0, height, posX, posY));
            page.Contents.Insert(2, new Do(imageName));
            page.Contents.Insert(3, new GRestore());

            // Save the modified PDF (lifecycle rule: use using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"QR code embedded and saved to '{outputPdfPath}'.");
    }
}
