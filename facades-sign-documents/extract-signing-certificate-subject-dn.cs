using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the signed PDF (adjust as needed)
        const string pdfPath = "signed_document.pdf";

        // Ensure the PDF file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF and extract the certificate of the first signature
        X509Certificate2 certificate = null;
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file for reading
            pdfSignature.BindPdf(pdfPath);

            // Get all signature names; assume at least one signature exists
            var signatureNames = pdfSignature.GetSignatureNames(false);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.Error.WriteLine("No signatures found in the PDF.");
                return;
            }

            // The first signature name (SignatureName object)
            var firstSignature = signatureNames[0];

            // Try to extract the certificate for the first signature
            if (!pdfSignature.TryExtractCertificate(firstSignature, out certificate) || certificate == null)
            {
                Console.Error.WriteLine("Failed to extract certificate from the signature.");
                return;
            }
        }

        // Retrieve the Subject Distinguished Name from the certificate
        string subjectDistinguishedName = certificate.Subject;

        // Store the subject name in an in‑memory DataTable (no external DB required)
        DataTable dt = new DataTable();
        dt.Columns.Add("SubjectDistinguishedName", typeof(string));
        dt.Rows.Add(subjectDistinguishedName);

        // Demonstrate that the data is stored (e.g., print the first row)
        Console.WriteLine("Extracted Subject DN: " + dt.Rows[0]["SubjectDistinguishedName"]);
        Console.WriteLine("Certificate subject distinguished name extracted and stored successfully.");
    }
}
