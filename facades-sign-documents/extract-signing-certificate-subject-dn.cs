using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the signed PDF file
        const string pdfPath = "signed_document.pdf";

        // Verify that the PDF file exists before trying to bind it
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: The file '{pdfPath}' was not found. Please provide a valid signed PDF file.");
            return;
        }

        // Use PdfFileSignature facade to work with signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            try
            {
                // Bind the PDF file for reading
                pdfSignature.BindPdf(pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to bind PDF file. Exception: {ex.Message}");
                return;
            }

            // Retrieve all non‑empty signature names (returns IList<SignatureName>)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the PDF.");
                return;
            }

            // For demonstration, process the first signature found
            SignatureName firstSignature = signatureNames[0];
            string signatureDisplayName = firstSignature.ToString();

            // Try to extract the X.509 certificate associated with the signature
            if (pdfSignature.TryExtractCertificate(firstSignature, out X509Certificate2 certificate) && certificate != null)
            {
                // The Subject property contains the distinguished name (e.g., "CN=John Doe, O=Company, C=US")
                string subjectDistinguishedName = certificate.Subject;

                Console.WriteLine($"Signature '{signatureDisplayName}' subject DN: {subjectDistinguishedName}");

                // Store the subject DN in an in‑memory DataTable (no external SQL client required)
                DataTable dt = new DataTable();
                dt.Columns.Add("SubjectDistinguishedName", typeof(string));
                dt.Rows.Add(subjectDistinguishedName);

                // Example of using the DataTable – here we just print the stored value
                Console.WriteLine("Subject distinguished name saved to in‑memory table.");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"[Table] SubjectDistinguishedName = {row["SubjectDistinguishedName"]}");
                }
            }
            else
            {
                Console.WriteLine($"Failed to extract certificate for signature '{signatureDisplayName}'.");
            }
        }
    }
}
