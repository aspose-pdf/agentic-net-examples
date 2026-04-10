using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputPdf = "signature_report.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF that contains signatures
        using (Document srcDoc = new Document(inputPdf))
        {
            // Initialize the PdfFileSignature facade
            PdfFileSignature signatureFacade = new PdfFileSignature();
            signatureFacade.BindPdf(srcDoc);

            // Retrieve all non‑empty signature names
            IList<SignatureName> signatureNames = signatureFacade.GetSignatureNames();

            // Prepare the report content
            var reportLines = new List<string>();
            reportLines.Add("Signature Report");
            reportLines.Add("================");
            reportLines.Add($"Source PDF: {Path.GetFileName(inputPdf)}");
            reportLines.Add($"Total Signatures: {signatureNames.Count}");
            reportLines.Add(string.Empty);

            foreach (SignatureName sigName in signatureNames)
            {
                // Gather signature details
                string signer = signatureFacade.GetSignerName(sigName) ?? "N/A";
                string reason = signatureFacade.GetReason(sigName) ?? "N/A";
                string location = signatureFacade.GetLocation(sigName) ?? "N/A";
                DateTime? dateTime = signatureFacade.GetDateTime(sigName);
                bool isValid = signatureFacade.VerifySignature(sigName);
                int revision = signatureFacade.GetRevision(sigName);
                bool coversWholeDoc = signatureFacade.CoversWholeDocument(sigName);

                reportLines.Add($"Signature Name   : {sigName}");
                reportLines.Add($"  Signer         : {signer}");
                reportLines.Add($"  Reason         : {reason}");
                reportLines.Add($"  Location       : {location}");
                reportLines.Add($"  Date/Time      : {(dateTime.HasValue ? dateTime.Value.ToString("u") : "N/A")}");
                reportLines.Add($"  Revision       : {revision}");
                reportLines.Add($"  Covers Whole Document: {coversWholeDoc}");
                reportLines.Add($"  Verification   : {(isValid ? "Valid" : "Invalid")}");
                reportLines.Add(string.Empty);
            }

            // Create a new PDF document for the report
            using (Document reportDoc = new Document())
            {
                // Add a page to the report document
                Page page = reportDoc.Pages.Add();

                // Combine all lines into a single string with line breaks
                string reportText = string.Join("\n", reportLines);

                // Create a TextFragment to hold the report text
                TextFragment fragment = new TextFragment(reportText)
                {
                    // Optional styling
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                };

                // Add the text fragment to the page
                page.Paragraphs.Add(fragment);

                // Save the report PDF
                reportDoc.Save(outputPdf);
            }

            Console.WriteLine($"Signature report generated: {outputPdf}");
        }
    }
}