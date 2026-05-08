using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImage
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputPngPath = "signature_image.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Find the first signature field in the form
            SignatureField sigField = null;
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField sField)
                {
                    sigField = sField;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the document.");
                return;
            }

            // Extract the signature image as a JPEG encoded stream
            Stream jpegStream = sigField.ExtractImage();
            if (jpegStream == null)
            {
                Console.Error.WriteLine("Signature image could not be extracted.");
                return;
            }

            // Ensure the stream is positioned at the beginning
            jpegStream.Position = 0;

            // Load the JPEG stream into a System.Drawing.Image and save as PNG
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(jpegStream))
            {
                img.Save(outputPngPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Signature image saved as PNG to '{outputPngPath}'.");
    }
}
