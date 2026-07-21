using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string signatureFieldName = "Signature1"; // replace with actual field name
        const string outputPng = "signature_image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the signature field by name
            if (doc.Form[signatureFieldName] is SignatureField sigField)
            {
                // Extract the signature appearance image as a JPEG-encoded stream
                using (Stream jpegStream = sigField.ExtractImage())
                {
                    if (jpegStream == null)
                    {
                        Console.Error.WriteLine("No signature image found in the field.");
                        return;
                    }

                    // Ensure the stream position is at the beginning
                    jpegStream.Position = 0;

                    // Load the JPEG stream into a System.Drawing.Image (fully qualified to avoid ambiguity)
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(jpegStream))
                    {
                        // Save the image as PNG
                        img.Save(outputPng, ImageFormat.Png);
                        Console.WriteLine($"Signature image saved to '{outputPng}'.");
                    }
                }
            }
            else
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found or is not a SignatureField.");
            }
        }
    }
}
