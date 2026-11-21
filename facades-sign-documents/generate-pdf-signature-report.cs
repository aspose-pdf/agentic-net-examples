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

        // Initialize PdfFileSignature using the non‑obsolete pattern
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdf);

        // Retrieve the list of signature names (includes valid and invalid signatures)
        var signatureNames = pdfSignature.GetSignatureNames(true);

        // Create a new PDF document for the report
        using (Document report = new Document())
        {
            // Add the first page
            Page page = report.Pages.Add();

            // Title
            TextFragment title = new TextFragment("Signature Report");
            title.TextState.FontSize = 18;
            title.TextState.FontStyle = FontStyles.Bold;
            title.Position = new Position(0, page.PageInfo.Height - 50);
            title.HorizontalAlignment = HorizontalAlignment.Center;
            page.Paragraphs.Add(title);

            // Starting Y coordinate for the list
            double y = page.PageInfo.Height - 80;

            foreach (var sigName in signatureNames)
            {
                // Verify the signature using the facade
                bool isValid = pdfSignature.VerifySignature(sigName);

                // Retrieve signer information (if available)
                string signer = pdfSignature.GetSignerName(sigName) ?? "Unknown";
                DateTime? signDate = null;
                try
                {
                    signDate = pdfSignature.GetDateTime(sigName);
                }
                catch { /* GetDateTime may throw for malformed signatures; ignore */ }

                // Prepare line text
                string line = $"Signer: {signer}, Verified: {isValid}";
                if (signDate.HasValue)
                {
                    line += $", Date: {signDate.Value:G}";
                }

                // Add line to the page
                TextFragment tf = new TextFragment(line);
                tf.Position = new Position(20, y);
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);

                y -= 20; // Move down for next entry

                // Add a new page if we run out of space
                if (y < 50)
                {
                    page = report.Pages.Add();
                    y = page.PageInfo.Height - 50;
                }
            }

            // Save the report PDF
            report.Save(reportPdf);
        }

        Console.WriteLine($"Signature report saved to '{reportPdf}'.");
    }
}
