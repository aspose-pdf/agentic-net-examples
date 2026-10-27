using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputPng = "extracted_signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document contains at least one signature field
                if (doc?.Form?.Fields == null)
                {
                    Console.Error.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Retrieve the first signature field (signature fields are stored in the generic Fields collection)
                SignatureField sigField = doc.Form.Fields
                                            .OfType<SignatureField>()
                                            .FirstOrDefault();

                if (sigField == null)
                {
                    Console.Error.WriteLine("No signature fields found in the PDF.");
                    return;
                }

                // Extract the appearance image as a stream. The stream already contains the image data (usually PNG).
                using (Stream imgStream = sigField.ExtractImage())
                {
                    if (imgStream == null)
                    {
                        Console.Error.WriteLine("Signature appearance does not contain an image.");
                        return;
                    }

                    // Write the stream to the output file
                    using (FileStream fs = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
                    {
                        imgStream.CopyTo(fs);
                    }
                }

                Console.WriteLine($"Signature image extracted to '{outputPng}'.");
            }
        }
        // GDI+ related exceptions are no longer relevant because we avoid System.Drawing.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
