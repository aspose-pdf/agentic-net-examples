using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string reportPdf = "signature_report.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF and extract signature information
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Get all active signature names
            var signatureNames = pdfSignature.GetSignatureNames();

            // Prepare a new PDF document for the report
            using (Document reportDoc = new Document())
            {
                // Add a page to the report
                Page page = reportDoc.Pages.Add();

                // Title
                TextFragment title = new TextFragment("Signature Report")
                {
                    TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
                };
                page.Paragraphs.Add(title);
                page.Paragraphs.Add(new TextFragment(Environment.NewLine));

                // Total revisions
                int totalRevisions = pdfSignature.GetTotalRevision();
                TextFragment totalRevFragment = new TextFragment($"Total Revisions: {totalRevisions}");
                page.Paragraphs.Add(totalRevFragment);
                page.Paragraphs.Add(new TextFragment(Environment.NewLine));

                // Iterate over each signature and collect details
                foreach (var sigNameObj in signatureNames)
                {
                    // SignatureName may be a custom type; use its string representation
                    string sigName = sigNameObj.ToString();

                    // Gather details
                    string signer = pdfSignature.GetSignerName(sigName);
                    bool isValid = pdfSignature.VerifySignature(sigName);
                    string reason = pdfSignature.GetReason(sigName);
                    string location = pdfSignature.GetLocation(sigName);
                    DateTime? dateTime = pdfSignature.GetDateTime(sigName);
                    int revision = pdfSignature.GetRevision(sigName);

                    // Build report entry
                    string entry = $"Signature Name : {sigName}{Environment.NewLine}" +
                                   $"Signer         : {signer}{Environment.NewLine}" +
                                   $"Valid          : {isValid}{Environment.NewLine}" +
                                   $"Reason         : {reason}{Environment.NewLine}" +
                                   $"Location       : {location}{Environment.NewLine}" +
                                   $"Date/Time      : {(dateTime.HasValue ? dateTime.Value.ToString("u") : "N/A")}{Environment.NewLine}" +
                                   $"Revision       : {revision}{Environment.NewLine}" +
                                   $"----------------------------------------{Environment.NewLine}";

                    TextFragment fragment = new TextFragment(entry);
                    page.Paragraphs.Add(fragment);
                }

                // Save the report PDF
                reportDoc.Save(reportPdf);
            }
        }

        Console.WriteLine($"Signature report generated: {reportPdf}");
    }
}