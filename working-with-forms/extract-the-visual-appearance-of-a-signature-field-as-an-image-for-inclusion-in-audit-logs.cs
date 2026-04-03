using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";   // input PDF containing the signature field
        const string fieldName = "Signature1";          // name of the signature field to extract
        const string outputImage = "signature_appearance.jpg"; // output image file for audit log

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the field safely – the Form indexer returns a WidgetAnnotation, so cast to Field
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Signature field '{fieldName}' not found or not a form field.");
                return;
            }

            // Cast to SignatureField to access ExtractImage()
            if (field is SignatureField signatureField)
            {
                // Extract the visual appearance as a JPEG encoded stream
                using (Stream imageStream = signatureField.ExtractImage())
                {
                    if (imageStream == null)
                    {
                        Console.Error.WriteLine("No image found in the signature field.");
                        return;
                    }

                    // Save the extracted image to a file for audit logging
                    using (FileStream fileOut = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
                    {
                        imageStream.CopyTo(fileOut);
                    }

                    Console.WriteLine($"Signature appearance saved to '{outputImage}'.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a signature field.");
            }
        }
    }
}
