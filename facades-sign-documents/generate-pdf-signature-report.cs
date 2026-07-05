using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string reportPdf = "signature_report.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load signatures using PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Get all non‑empty signature names
            var sigNames = pdfSign.GetSignatureNames();

            // Create a new PDF document for the report
            using (Document reportDoc = new Document())
            {
                // Add the first page
                Page page = reportDoc.Pages.Add();

                // Header
                TextFragment header = new TextFragment("Signature Report");
                header.TextState.FontSize = 18;
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                header.Position = new Position(50, 800);
                page.Paragraphs.Add(header);

                double y = 760; // Starting Y position for entries

                foreach (var sigName in sigNames)
                {
                    // Retrieve signature details
                    string signer = pdfSign.GetSignerName(sigName);
                    string reason = pdfSign.GetReason(sigName);
                    string location = pdfSign.GetLocation(sigName);
                    DateTime? dateTime = pdfSign.GetDateTime(sigName);
                    bool isValid = pdfSign.VerifySignature(sigName);
                    int revision = pdfSign.GetRevision(sigName);
                    bool coversWhole = pdfSign.CoversWholeDocument(sigName);

                    // Build line text
                    string line = $"Name: {sigName}, Signer: {signer}, Reason: {reason}, Location: {location}, " +
                                  $"Date: {(dateTime.HasValue ? dateTime.Value.ToString() : "N/A")}, " +
                                  $"Valid: {isValid}, Revision: {revision}, CoversWhole: {coversWhole}";

                    TextFragment tf = new TextFragment(line);
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.Position = new Position(50, y);
                    page.Paragraphs.Add(tf);

                    y -= 20; // Move down for next entry

                    // Add a new page if needed
                    if (y < 50)
                    {
                        page = reportDoc.Pages.Add();
                        y = 800;
                    }
                }

                // Save the report PDF
                reportDoc.Save(reportPdf);
            }
        }

        Console.WriteLine($"Signature report saved to '{reportPdf}'.");
    }
}