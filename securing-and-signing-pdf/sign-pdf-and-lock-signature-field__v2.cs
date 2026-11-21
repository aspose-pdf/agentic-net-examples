using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_locked.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the source PDF into an expandable memory stream – required for signing.
            byte[] sourceBytes = File.ReadAllBytes(inputPath);
            using (var expandableStream = new MemoryStream())
            {
                // Copy the bytes into a stream that can grow.
                expandableStream.Write(sourceBytes, 0, sourceBytes.Length);
                expandableStream.Position = 0;

                using (var doc = new Document(expandableStream))
                {
                    // Create a signature field on the first page.
                    var sigField = new SignatureField(doc.Pages[1], new Rectangle(100, 100, 300, 200))
                    {
                        PartialName = "Signature1"
                    };
                    // Lock the field so the user cannot modify it after signing.
                    sigField.Flags = AnnotationFlags.Locked;

                    doc.Form.Add(sigField, 1);

                    // Prepare the PKCS#7 digital signature and set its metadata.
                    var pkcs7 = new PKCS7(certificatePath, certificatePassword)
                    {
                        Reason = "Document approval",
                        Location = "Office",
                        ContactInfo = "contact@example.com"
                    };

                    // Sign the field. The signing operation writes the signed PDF back into the source stream.
                    sigField.Sign(pkcs7);
                }

                // After signing, the stream now contains the signed PDF. Write it to the output file.
                File.WriteAllBytes(outputPath, expandableStream.ToArray());
            }

            Console.WriteLine($"Signed PDF saved to '{outputPath}'. Signature field is locked.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
