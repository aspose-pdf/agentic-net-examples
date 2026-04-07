using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputImagePath = "signature_appearance.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all form fields to locate a SignatureField
            SignatureField sigField = null;
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField sf)
                {
                    sigField = sf;
                    break; // take the first signature field found
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the document.");
                return;
            }

            // Extract the signature appearance as a JPEG image stream
            // Use the overload without parameters (defaults to JPEG)
            Stream imageStream = sigField.ExtractImage();

            // If you need a different format, e.g., PNG, use:
            // Stream imageStream = sigField.ExtractImage(ImageFormat.Png);

            if (imageStream == null)
            {
                Console.Error.WriteLine("Failed to extract the signature image.");
                return;
            }

            // Save the extracted image to a file for audit logging
            using (imageStream) // ensure the stream is closed after saving
            using (FileStream fileOut = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
            {
                imageStream.CopyTo(fileOut);
            }

            Console.WriteLine($"Signature appearance saved to '{outputImagePath}'.");
        }
    }
}