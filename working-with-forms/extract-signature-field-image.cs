using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "Signature1";               // name of the signature field
        const string outputImagePath = "signature_appearance.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the form contains the requested field
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Signature field '{fieldName}' not found.");
                return;
            }

            // Retrieve the field via the Form indexer (returns WidgetAnnotation) and cast to SignatureField
            if (doc.Form[fieldName] is not SignatureField sigField)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a signature field.");
                return;
            }

            // Extract the visual appearance of the signature field as a stream
            using (Stream imgStream = sigField.ExtractImage())
            {
                if (imgStream == null)
                {
                    Console.Error.WriteLine("Signature field has no appearance.");
                    return;
                }

                // Save the appearance stream to a PNG file for audit logs
                using (FileStream fs = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(fs);
                }
                Console.WriteLine($"Signature appearance saved to '{outputImagePath}'.");

                // Optionally, obtain the image bytes for further processing (e.g., storing in a DB)
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    // Reset the position of the original stream if needed
                    imgStream.Position = 0;
                    imgStream.CopyTo(ms);
                    imageBytes = ms.ToArray();
                }
                // imageBytes now contains the PNG data of the signature appearance
                // (you can use it as needed, e.g., embed in audit log entries)
            }
        }
    }
}
