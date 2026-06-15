using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";
        const string reportPdf = "signature_report.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the source PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            // Build the report text
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Signature Summary Report");
            reportBuilder.AppendLine("========================");
            reportBuilder.AppendLine();

            if (signatureNames.Count == 0)
            {
                reportBuilder.AppendLine("No signatures were found in the document.");
            }
            else
            {
                for (int i = 0; i < signatureNames.Count; i++)
                {
                    var sigName = signatureNames[i];

                    // Gather details for each signature
                    string signer   = pdfSign.GetSignerName(sigName) ?? "N/A";
                    string reason   = pdfSign.GetReason(sigName) ?? "N/A";
                    string location = pdfSign.GetLocation(sigName) ?? "N/A";

                    // GetDateTime returns a non‑nullable DateTime; treat MinValue as "not set"
                    DateTime dt = pdfSign.GetDateTime(sigName);
                    string dateTime = dt != DateTime.MinValue ? dt.ToString() : "N/A";

                    bool   valid    = pdfSign.VerifySignature(sigName);
                    int    revision = pdfSign.GetRevision(sigName);
                    bool   coversWhole = pdfSign.CoversWholeDocument(sigName);

                    reportBuilder.AppendLine($"Signature {i + 1}:");
                    reportBuilder.AppendLine($"  Name          : {sigName}");
                    reportBuilder.AppendLine($"  Signer        : {signer}");
                    reportBuilder.AppendLine($"  Reason        : {reason}");
                    reportBuilder.AppendLine($"  Location      : {location}");
                    reportBuilder.AppendLine($"  Date/Time     : {dateTime}");
                    reportBuilder.AppendLine($"  Valid         : {valid}");
                    reportBuilder.AppendLine($"  Revision      : {revision}");
                    reportBuilder.AppendLine($"  Covers Whole  : {coversWhole}");
                    reportBuilder.AppendLine();
                }

                // Total revisions in the document
                int totalRevisions = pdfSign.GetTotalRevision();
                reportBuilder.AppendLine($"Total Document Revisions: {totalRevisions}");
            }

            // Create a new PDF document for the report
            using (Document reportDoc = new Document())
            {
                // Add a page
                Page page = reportDoc.Pages.Add();

                // Create a text fragment with the report content
                TextFragment tf = new TextFragment(reportBuilder.ToString())
                {
                    // Use a readable font and size
                    TextState = { Font = FontRepository.FindFont("Helvetica"), FontSize = 12, ForegroundColor = Color.Black }
                };

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                // Save the report PDF
                reportDoc.Save(reportPdf);
            }
        }

        Console.WriteLine($"Signature report generated: {reportPdf}");
    }
}
