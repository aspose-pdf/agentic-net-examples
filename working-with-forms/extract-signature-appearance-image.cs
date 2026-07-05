using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "signed_document.pdf";
        const string outputImgPath = "signature_appearance.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all form fields to locate a SignatureField
            SignatureField sigField = null;
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (field is SignatureField sf)
                {
                    sigField = sf;
                    break; // assume first signature field is the target
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the document.");
                return;
            }

            // Extract the visual appearance as an image stream (PNG format)
            using (Stream imgStream = sigField.ExtractImage(ImageFormat.Png))
            {
                if (imgStream == null)
                {
                    Console.Error.WriteLine("Signature image could not be extracted.");
                    return;
                }

                // Write the stream to a file for audit logging
                using (FileStream fileOut = new FileStream(outputImgPath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(fileOut);
                }
            }
        }

        Console.WriteLine($"Signature appearance saved to '{outputImgPath}'.");
    }
}