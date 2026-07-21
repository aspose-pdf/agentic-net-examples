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

        // Load the signed PDF and extract signature information
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            // Create a new PDF document for the report
            using (Document reportDoc = new Document())
            {
                // Add a page to the report
                Page reportPage = reportDoc.Pages.Add();

                // Title
                TextFragment title = new TextFragment("Signature Report");
                title.TextState.FontSize = 18;
                title.TextState.FontStyle = FontStyles.Bold;
                title.Position = new Position(50, 800);
                reportPage.Paragraphs.Add(title);

                // Header line
                TextFragment header = new TextFragment(
                    "Name | Signer | Valid | Reason | Location | DateTime | Revision | Covers Whole Document");
                header.TextState.FontSize = 12;
                header.TextState.FontStyle = FontStyles.Bold;
                header.Position = new Position(50, 770);
                reportPage.Paragraphs.Add(header);

                // Iterate over each signature and add its details
                float yPos = 750;
                for (int i = 0; i < signatureNames.Count; i++)
                {
                    var sigName = signatureNames[i];

                    string signer = pdfSign.GetSignerName(sigName) ?? "N/A";
                    bool isValid = pdfSign.VerifySignature(sigName);
                    string reason = pdfSign.GetReason(sigName) ?? "N/A";
                    string location = pdfSign.GetLocation(sigName) ?? "N/A";
                    DateTime dateTime = pdfSign.GetDateTime(sigName);
                    int revision = pdfSign.GetRevision(sigName);
                    bool coversWhole = pdfSign.CoversWholeDocument(sigName);

                    string line = $"{sigName} | {signer} | {isValid} | {reason} | {location} | {dateTime:G} | {revision} | {coversWhole}";
                    TextFragment tf = new TextFragment(line);
                    tf.TextState.FontSize = 10;
                    tf.Position = new Position(50, yPos);
                    reportPage.Paragraphs.Add(tf);

                    yPos -= 20; // Move down for next line
                    if (yPos < 50) // Add a new page if needed
                    {
                        reportPage = reportDoc.Pages.Add();
                        yPos = 800;
                    }
                }

                // Add total revision information
                int totalRevision = pdfSign.GetTotalRevision();
                TextFragment total = new TextFragment($"Total Document Revisions: {totalRevision}");
                total.TextState.FontSize = 12;
                total.TextState.FontStyle = FontStyles.Bold;
                total.Position = new Position(50, yPos - 30);
                reportPage.Paragraphs.Add(total);

                // Save the report PDF
                reportDoc.Save(reportPdf);
            }
        }

        Console.WriteLine($"Signature report generated: {reportPdf}");
    }
}