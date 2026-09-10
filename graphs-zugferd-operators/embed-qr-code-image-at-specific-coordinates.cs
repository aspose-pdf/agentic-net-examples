using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string qrImagePath = "qr.png";

        // Define the rectangle where the QR code will be placed (lower‑left and upper‑right corners)
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 200; // upper‑right X
        double ury = 600; // upper‑right Y

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

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Calculate width and height from the rectangle
            double width = urx - llx;
            double height = ury - lly;

            // Create an ImageStamp for the QR code image
            ImageStamp qrStamp = new ImageStamp(qrImagePath)
            {
                // Position the stamp using the lower‑left corner coordinates
                XIndent = llx,
                YIndent = lly,
                // Set the desired size (width/height)
                Width = width,
                Height = height,
                // Ensure the stamp is placed exactly at the coordinates (no alignment offsets)
                HorizontalAlignment = HorizontalAlignment.None,
                VerticalAlignment = VerticalAlignment.None
            };

            // Add the stamp to the page content
            page.AddStamp(qrStamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"QR code embedded successfully: {outputPdfPath}");
    }
}
