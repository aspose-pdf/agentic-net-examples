using System;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImage
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";   // input PDF containing a signature field
        const string fieldName = "Signature1";          // name of the signature field to extract
        const string outputPng = "signature_image.png"; // output PNG file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the signature field by name; cast to SignatureField
            if (doc.Form[fieldName] is SignatureField sigField)
            {
                // Extract the appearance image as a JPEG encoded stream
                using (Stream jpegStream = sigField.ExtractImage())
                {
                    if (jpegStream == null)
                    {
                        Console.Error.WriteLine("No signature image found in the field.");
                        return;
                    }

                    // Ensure the stream is positioned at the beginning
                    jpegStream.Position = 0;

                    // Load the JPEG stream into a System.Drawing.Image (fully qualified to avoid ambiguity)
                    using (System.Drawing.Image jpegImage = System.Drawing.Image.FromStream(jpegStream))
                    {
                        // Save the image as PNG
                        jpegImage.Save(outputPng, System.Drawing.Imaging.ImageFormat.Png);
                        Console.WriteLine($"Signature image saved as PNG: {outputPng}");
                    }
                }
            }
            else
            {
                Console.Error.WriteLine($"Signature field '{fieldName}' not found.");
            }
        }
    }
}
