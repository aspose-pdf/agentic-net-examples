using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Path to the signed PDF file
        const string pdfPath = "signed_document.pdf";

        // Ensure the PDF file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (required to access form fields)
        Document pdfDoc = new Document(pdfPath);

        // Prepare an in‑memory DataTable to mimic database storage
        DataTable dt = new DataTable();
        dt.Columns.Add("SignatureName", typeof(string));
        dt.Columns.Add("SubjectDistinguishedName", typeof(string));

        // Iterate over all form fields and extract certificates from signature fields
        foreach (Field field in pdfDoc.Form.Fields)
        {
            if (field is SignatureField sigField)
            {
                // Extract the signing certificate from the signature field
                X509Certificate2 cert = sigField.ExtractCertificateObject();
                if (cert == null)
                {
                    Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a certificate.");
                    continue;
                }

                // Use the field's name as the signature identifier
                string sigName = sigField.PartialName;
                string subjectDn = cert.Subject; // e.g., "CN=John Doe, O=Company, C=US"

                // Store the data in the in‑memory DataTable (simulating a DB)
                dt.Rows.Add(sigName, subjectDn);
                Console.WriteLine($"Signature '{sigName}' subject DN stored: {subjectDn}");
            }
        }

        // If no signature fields were found, inform the user
        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No digital signatures with certificates were found in the PDF.");
            return;
        }

        // Example: display the collected rows (simulating a DB read)
        Console.WriteLine("\nCollected signature data:");
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"Name: {row["SignatureName"]}, Subject DN: {row["SubjectDistinguishedName"]}");
        }
    }
}
