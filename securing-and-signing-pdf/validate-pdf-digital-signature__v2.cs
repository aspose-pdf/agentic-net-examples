using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                var form = doc.Form;

                if (form?.Fields == null)
                {
                    Console.WriteLine("No form fields found in the document.");
                    return;
                }

                // Iterate over all fields and process only signature fields
                foreach (var field in form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        // Verify the digital signature using the simple overload (no extra validation options)
                        bool isValid = false;
                        try
                        {
                            if (sigField.Signature != null)
                            {
                                isValid = sigField.Signature.Verify();
                            }
                            else
                            {
                                Console.WriteLine($"Signature object not present for field '{sigField.PartialName}'.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Verification error for field '{sigField.PartialName}': {ex.Message}");
                        }

                        // Retrieve the signing certificate (X509Certificate2) if present
                        X509Certificate2 cert = null;
                        try
                        {
                            cert = sigField.ExtractCertificateObject();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Certificate extraction error for field '{sigField.PartialName}': {ex.Message}");
                        }

                        // Output validation result and certificate details
                        Console.WriteLine($"Signature Field: {sigField.PartialName ?? "<unnamed>"}");
                        Console.WriteLine($"  Valid: {isValid}");
                        if (cert != null)
                        {
                            Console.WriteLine($"  Subject: {cert.Subject}");
                            Console.WriteLine($"  Issuer : {cert.Issuer}");
                            Console.WriteLine($"  Valid From: {cert.NotBefore}");
                            Console.WriteLine($"  Valid To  : {cert.NotAfter}");
                            Console.WriteLine($"  Thumbprint: {cert.Thumbprint}");
                        }
                        else
                        {
                            Console.WriteLine("  No signing certificate found.");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
