using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing the signature field
        const string outputPngPath = "WitnessSignature.png"; // destination PNG file
        const string signatureFieldName = "WitnessSignature";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfFileSignature facade to work with the signature field
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF document
            pdfSignature.BindPdf(inputPdfPath);

            // Extract the image stream associated with the specified signature field.
            // The method returns a JPEG‑encoded stream. Use the overload that accepts the field name as a string.
            using (Stream jpegStream = pdfSignature.ExtractImage(signatureFieldName))
            {
                if (jpegStream == null)
                {
                    Console.Error.WriteLine($"No image found for signature field '{signatureFieldName}'.");
                    return;
                }

                // Convert the JPEG stream to a PNG file.
                // System.Drawing.Image is used here for format conversion.
                using (Image img = Image.FromStream(jpegStream))
                {
                    img.Save(outputPngPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Signature image saved as PNG to '{outputPngPath}'.");
    }
}
