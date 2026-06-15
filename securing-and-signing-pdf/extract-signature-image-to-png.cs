using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for PNG
using Aspose.Pdf;                           // Core PDF API
using Aspose.Pdf.Forms;                     // SignatureField type

class ExtractSignatureImage
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";   // source PDF with a signature field
        const string outputPng = "signature_image.png";   // destination PNG file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over all form fields and locate the first signature field
            SignatureField sigField = null;
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (field is SignatureField sf)
                {
                    sigField = sf;
                    break; // use the first signature field found
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the document.");
                return;
            }

            // Extract the signature appearance image as a PNG-encoded stream
            // The overload accepts System.Drawing.Imaging.ImageFormat
            using (Stream imageStream = sigField.ExtractImage(ImageFormat.Png))
            {
                if (imageStream == null)
                {
                    Console.Error.WriteLine("Signature image could not be extracted.");
                    return;
                }

                // Save the PNG stream to a file
                using (FileStream fileOut = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(fileOut);
                }
            }
        }

        Console.WriteLine($"Signature image extracted and saved to '{outputPng}'.");
    }
}