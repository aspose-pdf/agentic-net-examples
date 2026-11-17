using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class BatchPdfSigner
{
    // Placeholder for certificate retrieval based on document type metadata.
    // In a real scenario this would query a database.
    private static (string certPath, string certPassword) GetCertificateInfo(string documentType)
    {
        // Example mapping – replace with actual DB lookup.
        if (documentType == "Invoice")
            return (@"C:\Certificates\invoice_cert.pfx", "invoicePwd");
        if (documentType == "Report")
            return (@"C:\Certificates\report_cert.pfx", "reportPwd");
        // Default certificate.
        return (@"C:\Certificates\default_cert.pfx", "defaultPwd");
    }

    // Adds a visible signature field to the first page and signs the document.
    private static void SignPdf(string inputPdfPath, string outputPdfPath)
    {
        // Open the PDF document.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Retrieve a document type identifier from metadata (e.g., Title).
            string docType = doc.Info.Title ?? "Default";

            // Obtain the certificate path and password for this document type.
            (string certPath, string certPassword) = GetCertificateInfo(docType);

            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
                throw new InvalidOperationException("PDF contains no pages.");

            // Create a signature field on the first page.
            // Position rectangle: lower-left (100,100), upper-right (200,150).
            Aspose.Pdf.Forms.SignatureField signatureField = new Aspose.Pdf.Forms.SignatureField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 100, 200, 150));

            // Add the signature field to the page's annotations collection.
            doc.Pages[1].Annotations.Add(signatureField);

            // Sign the field using the PKCS#7 certificate.
            Aspose.Pdf.Forms.PKCS7 pkcs7 = new Aspose.Pdf.Forms.PKCS7(certPath, certPassword);
            signatureField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdfPath);
        }
    }

    static void Main()
    {
        // Folder containing PDFs to be signed.
        string sourceFolder = @"C:\PdfToSign";
        // Folder where signed PDFs will be saved.
        string destinationFolder = @"C:\SignedPdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
            return;
        }

        Directory.CreateDirectory(destinationFolder);

        // Collect all PDF files in the source folder.
        List<string> pdfFiles = new List<string>(Directory.GetFiles(sourceFolder, "*.pdf"));

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileName(pdfPath);
                string signedPath = Path.Combine(destinationFolder, fileName);

                SignPdf(pdfPath, signedPath);
                Console.WriteLine($"Signed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to sign '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch signing completed.");
    }
}