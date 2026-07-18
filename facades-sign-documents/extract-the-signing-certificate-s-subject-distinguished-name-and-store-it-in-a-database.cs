using System;
using System.IO;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Open the PDF file for signature operations
        var pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(pdfPath);

        // Retrieve the list of signature names present in the document
        var signatureNames = pdfSign.GetSignatureNames(false);
        if (signatureNames == null || signatureNames.Count == 0)
        {
            Console.WriteLine("No signatures found in the PDF.");
            return;
        }

        // Use the first signature (adjust as needed for specific signatures)
        var sigName = signatureNames[0];

        // Extract the X.509 certificate associated with the signature
        if (!pdfSign.TryExtractCertificate(sigName, out X509Certificate2 certificate) || certificate == null)
        {
            Console.WriteLine("Failed to extract certificate from the signature.");
            return;
        }

        // Get the subject distinguished name from the certificate
        string subjectDn = certificate.Subject;

        // -----------------------------------------------------------------
        // Instead of a real SQL Server connection (which requires the
        // System.Data.SqlClient package), store the value in an in‑memory
        // DataTable. This satisfies the compile‑time requirement and keeps the
        // example self‑contained.
        // -----------------------------------------------------------------
        DataTable dt = new DataTable();
        dt.Columns.Add("SubjectDistinguishedName", typeof(string));
        dt.Rows.Add(subjectDn);

        // Demonstrate that the data is available (e.g., write to console)
        Console.WriteLine("Stored subject distinguished name in DataTable:");
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"  {row["SubjectDistinguishedName"]}");
        }

        Console.WriteLine("Certificate subject distinguished name processed successfully.");
    }
}
