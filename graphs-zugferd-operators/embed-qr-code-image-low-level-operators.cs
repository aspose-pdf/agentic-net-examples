using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output_with_qr.pdf"; // result PDF
        const string qrImagePath    = "qr_code.png";        // QR code image file (PNG, JPG, etc.)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the QR code will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Add the QR code image to the page resources and obtain its name
            string imageResourceName;
            using (FileStream imgStream = File.OpenRead(qrImagePath))
            {
                imageResourceName = page.Resources.Images.Add(imgStream);
            }

            // Retrieve the image object to obtain its original dimensions
            XImage xImg = page.Resources.Images[imageResourceName];

            // Define the rectangle (coordinates in user space) where the image should appear
            // Example: lower‑left (100, 500), upper‑right (200, 600)
            double llx = 100;
            double lly = 500;
            double urx = 200;
            double ury = 600;
            Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Compute scaling factors based on desired rectangle size vs. original image size
            float targetWidth  = (float)(imgRect.URX - imgRect.LLX);
            float targetHeight = (float)(imgRect.URY - imgRect.LLY);
            float scaleX = targetWidth  / xImg.Width;
            float scaleY = targetHeight / xImg.Height;

            // Build the low‑level PDF operators using the correct Aspose.Pdf.Operators classes:
            // GSave (q) – save graphics state
            // ConcatenateMatrix (cm) – modify the current transformation matrix (scale + translate)
            // Do – draw the image XObject
            // GRestore (Q) – restore graphics state
            List<Operator> ops = new List<Operator>
            {
                new GSave(),
                new ConcatenateMatrix(scaleX, 0, 0, scaleY, (float)imgRect.LLX, (float)imgRect.LLY),
                new Do(imageResourceName),
                new GRestore()
            };

            // Insert the operators at the end of the page's content stream
            OperatorCollection content = page.Contents;
            content.Insert(content.Count, ops.ToArray());

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"QR code image embedded and saved to '{outputPdfPath}'.");
    }
}
