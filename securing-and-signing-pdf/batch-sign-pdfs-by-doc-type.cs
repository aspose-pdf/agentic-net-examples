using System;
using System.Collections.Generic;
using System.Data;               // For database access (mocked)
using System.Data.SqlClient;    // Adjust provider as needed
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfSigner
{
    // Mock method: retrieve certificate stream and password for a given document type.
    // In a real scenario replace with actual DB logic.
    private static (Stream certStream, string password) GetCertificateForDocType(string docType)
    {
        // Example: fetch from a SQL database
        // string connectionString = "...";
        // using (SqlConnection conn = new SqlConnection(connectionString))
        // {
        //     conn.Open();
        //     using (SqlCommand cmd = new SqlCommand("SELECT CertData, CertPassword FROM Certificates WHERE DocType = @type", conn))
        //     {
        //         cmd.Parameters.AddWithValue("@type", docType);
        //         using (SqlDataReader rdr = cmd.ExecuteReader())
        //         {
        //             if (rdr.Read())
        //             {
        //                 byte[] certBytes = (byte[])rdr["CertData"];
        //                 string pwd = rdr["CertPassword"] as string;
        //                 return (new MemoryStream(certBytes), pwd);
        //             }
        //         }
        //     }
        // }
        // Placeholder: use a local .pfx file for demonstration
        string pfxPath = $"certs/{docType}.pfx";
        string pwd = "pfxPassword"; // replace with actual password
        return (File.OpenRead(pfxPath), pwd);
    }

    // Signs a single PDF file using the appropriate certificate.
    private static void SignPdf(string inputPdfPath, string outputPdfPath, string docType)
    {
        // Retrieve certificate data for the document type
        var (certStream, certPassword) = GetCertificateForDocType(docType);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document contains form fields
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine($"No form fields found in '{inputPdfPath}'. Skipping.");
                return;
            }

            // Find the first signature field (adjust logic if multiple fields are needed)
            SignatureField sigField = null;
            foreach (var field in pdfDoc.Form)
            {
                if (field is SignatureField sf)
                {
                    sigField = sf;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine($"No signature field found in '{inputPdfPath}'. Skipping.");
                return;
            }

            // Create a concrete PKCS7 signature object using the certificate stream and password
            PKCS7 pkcs7 = new PKCS7(certStream, certPassword)
            {
                Reason = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@company.com",
                // Date property is optional; if not available, omit it.
                // Date = DateTime.UtcNow
            };

            // Sign the field with the PKCS7 signature
            sigField.Sign(pkcs7);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        // Dispose of the certificate stream
        certStream.Dispose();
    }

    // Entry point: batch process a list of PDFs.
    static void Main()
    {
        // Mapping of input PDF paths to their document type metadata
        var pdfJobs = new List<(string inputPath, string outputPath, string docType)>
        {
            ("Docs/Invoice_001.pdf", "Signed/Invoice_001_signed.pdf", "Invoice"),
            ("Docs/Contract_ABC.pdf", "Signed/Contract_ABC_signed.pdf", "Contract"),
            ("Docs/Report_Q1.pdf",   "Signed/Report_Q1_signed.pdf",   "Report")
        };

        foreach (var (input, output, type) in pdfJobs)
        {
            if (!File.Exists(input))
            {
                Console.WriteLine($"File not found: {input}");
                continue;
            }

            try
            {
                SignPdf(input, output, type);
                Console.WriteLine($"Signed PDF saved to '{output}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error signing '{input}': {ex.Message}");
            }
        }
    }
}
