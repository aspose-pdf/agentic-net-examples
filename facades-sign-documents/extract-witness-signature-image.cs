using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputImagePath = "WitnessSignature.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF using the PdfFileSignature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(pdfPath);

            // Identify the signature field by name
            string sigName = "WitnessSignature";

            // Extract the signature image (returned as a JPEG stream)
            using (Stream imgStream = pdfSignature.ExtractImage(sigName))
            {
                if (imgStream == null)
                {
                    Console.Error.WriteLine("Signature image not found.");
                    return;
                }

                // Convert the JPEG stream to PNG and save it
                using (Image image = Image.FromStream(imgStream))
                {
                    image.Save(outputImagePath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Signature image saved to '{outputImagePath}'.");
    }
}
