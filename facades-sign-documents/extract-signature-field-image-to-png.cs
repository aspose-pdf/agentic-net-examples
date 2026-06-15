using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing the signature field
        const string outputPng = "WitnessSignature.png"; // Desired PNG output file
        const string fieldName = "WitnessSignature";   // Name of the signature field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade and extract the image of the specified signature field
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // ExtractImage has an overload that accepts the field name directly (no SignatureName object needed)
            using (Stream jpegStream = pdfSignature.ExtractImage(fieldName))
            {
                if (jpegStream == null)
                {
                    Console.Error.WriteLine($"No image found for signature field '{fieldName}'.");
                    return;
                }

                // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(jpegStream))
                {
                    img.Save(outputPng, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Signature image saved as PNG to '{outputPng}'.");
    }
}
