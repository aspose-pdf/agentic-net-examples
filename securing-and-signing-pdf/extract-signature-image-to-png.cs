using System;
using System.IO;
using System.Drawing.Imaging;          // for ImageFormat
using System.Linq;                     // for Any()
using Aspose.Pdf;                     // core PDF classes
using Aspose.Pdf.Forms;               // SignatureField

class ExtractSignatureImage
{
    static void Main()
    {
        const string pdfPath   = "signed_document.pdf";
        const string fieldName = "Signature1";   // name of the signature field
        const string outputPng = "signature_image.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Verify that the form contains a field with the requested name
                bool fieldExists = doc?.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;
                if (!fieldExists)
                {
                    Console.Error.WriteLine($"Signature field '{fieldName}' not found.");
                    return;
                }

                // Retrieve the field and cast it to SignatureField
                SignatureField sigField = doc.Form[fieldName] as SignatureField;
                if (sigField == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a signature field.");
                    return;
                }

                // Extract the image as PNG (the overload accepts System.Drawing.Imaging.ImageFormat)
                using (Stream imgStream = sigField.ExtractImage(ImageFormat.Png))
                {
                    if (imgStream == null)
                    {
                        Console.Error.WriteLine("No image found in the signature appearance.");
                        return;
                    }

                    // Write the PNG stream to a file
                    using (FileStream fileOut = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
                    {
                        imgStream.CopyTo(fileOut);
                    }
                }

                Console.WriteLine($"Signature image saved to '{outputPng}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
