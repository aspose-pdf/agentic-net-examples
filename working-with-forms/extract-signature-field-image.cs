using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputImagePath = "signature_image.jpg";
        const string signatureFieldName = "Signature1";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve the form field by name and cast to Field (WidgetAnnotation -> Field)
            Field? field = doc.Form[signatureFieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found or not a form field.");
                return;
            }

            // Ensure the field is a SignatureField
            if (field is not SignatureField sigField)
            {
                Console.Error.WriteLine($"Field '{signatureFieldName}' is not a signature field.");
                return;
            }

            // Extract the visual appearance as a JPEG stream
            using (Stream? imageStream = sigField.ExtractImage())
            {
                if (imageStream == null)
                {
                    Console.Error.WriteLine("No image data found in the signature field.");
                    return;
                }

                // Save the extracted image to a file for audit logging
                using (FileStream file = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(file);
                }

                Console.WriteLine($"Signature image extracted and saved to '{outputImagePath}'.");
            }
        }
    }
}
