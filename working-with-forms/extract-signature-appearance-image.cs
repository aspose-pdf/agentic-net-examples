using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputImage = "signature_image.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Locate the first signature field in the form collection
            SignatureField sigField = null;
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField s)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No signature field found in the document.");
                return;
            }

            // Extract the visual appearance of the signature as a JPEG stream
            using (Stream imgStream = sigField.ExtractImage())
            {
                if (imgStream == null)
                {
                    Console.WriteLine("Signature field does not contain an image.");
                    return;
                }

                // Save the extracted image to a file
                using (FileStream file = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(file);
                }

                Console.WriteLine($"Signature image saved to '{outputImage}'.");
            }
        }
    }
}