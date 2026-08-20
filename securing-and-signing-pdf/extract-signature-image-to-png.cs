using System;
using System.Drawing.Imaging; // ImageFormat for PNG
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";
        const string outputPng = "signature_image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the signature field by its name.
            // Adjust the field name ("Signature1") to match the actual name in your PDF.
            SignatureField sigField = doc.Form["Signature1"] as SignatureField;

            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field not found.");
                return;
            }

            // Extract the signature image directly as a PNG-encoded stream.
            // The overload accepts an ImageFormat, so we request PNG.
            using (Stream pngStream = sigField.ExtractImage(ImageFormat.Png))
            {
                if (pngStream == null)
                {
                    Console.Error.WriteLine("No signature image found in the field.");
                    return;
                }

                // Save the PNG stream to a file.
                using (FileStream file = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
                {
                    pngStream.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"Signature image saved to '{outputPng}'.");
    }
}