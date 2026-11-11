using System;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the signed PDF file
        const string pdfPath = "signed.pdf";

        // Ensure the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Create an in‑memory table to hold the results (no real DB required)
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("PdfFileName", typeof(string));
            resultTable.Columns.Add("CertificateSubject", typeof(string));

            // Use PdfFileSignature (the correct facade) to read signature information
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(pdfPath);

                // Get all signature names (the bool indicates whether to include empty signatures)
                var signatureNames = pdfSignature.GetSignatureNames(true);

                if (signatureNames == null || signatureNames.Count == 0)
                {
                    Console.WriteLine("No signatures found in the PDF.");
                }
                else
                {
                    foreach (SignatureName sigName in signatureNames)
                    {
                        // Extract the signing certificate as a stream
                        using (Stream certStream = pdfSignature.ExtractCertificate(sigName))
                        {
                            if (certStream != null && certStream.Length > 0)
                            {
                                // Load the certificate into X509Certificate2 to read the Subject DN
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    certStream.CopyTo(ms);
                                    X509Certificate2 cert = new X509Certificate2(ms.ToArray());
                                    string subjectDn = cert.Subject;

                                    // Add a row to the in‑memory table
                                    DataRow row = resultTable.NewRow();
                                    row["PdfFileName"] = Path.GetFileName(pdfPath);
                                    row["CertificateSubject"] = subjectDn;
                                    resultTable.Rows.Add(row);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Signature '{sigName}' does not contain a certificate.");
                            }
                        }
                    }
                }
            }

            // Output the collected data (simulating a DB insert)
            Console.WriteLine($"Collected {resultTable.Rows.Count} record(s):");
            foreach (DataRow r in resultTable.Rows)
            {
                Console.WriteLine($"File: {r["PdfFileName"]}, Subject DN: {r["CertificateSubject"]}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
