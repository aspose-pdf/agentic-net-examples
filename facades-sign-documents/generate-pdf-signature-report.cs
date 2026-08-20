using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class SignatureReportGenerator
{
    static void Main()
    {
        const string inputPdfPath  = "signed_document.pdf";      // source PDF with signatures
        const string outputPdfPath = "signature_report.pdf";     // generated report

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Bind the source PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdfPath);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            // Create a new PDF document for the report
            using (Document report = new Document())
            {
                // Add the first page
                Page page = report.Pages.Add();

                // Starting vertical position (top of the page)
                double yPos = 800;

                // Title of the report
                TextFragment title = new TextFragment("Signature Report");
                title.TextState.FontSize = 18;
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.Position = new Position(50, yPos);
                page.Paragraphs.Add(title);
                yPos -= 30; // move down after the title

                // Iterate over each signature and collect details
                foreach (SignatureName sigName in signatureNames)
                {
                    // Signature identifier (string representation)
                    string sigId = sigName.Name;

                    // Gather signature details
                    string signer   = pdfSign.GetSignerName(sigName) ?? "N/A";
                    DateTime? dt    = pdfSign.GetDateTime(sigName);
                    string dateStr  = dt?.ToString("g") ?? "N/A";
                    string reason   = pdfSign.GetReason(sigName) ?? "N/A";
                    string location = pdfSign.GetLocation(sigName) ?? "N/A";
                    bool   isValid  = pdfSign.VerifySignature(sigName);

                    // Compose a line with the collected information
                    string line = $"Signature: {sigId} | Signer: {signer} | Date: {dateStr} | Reason: {reason} | Location: {location} | Valid: {isValid}";

                    // Add the line to the report
                    TextFragment tf = new TextFragment(line);
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.Position = new Position(50, yPos);
                    page.Paragraphs.Add(tf);
                    yPos -= 20; // move down for the next entry

                    // If we reach the bottom margin, start a new page
                    if (yPos < 50)
                    {
                        page = report.Pages.Add();
                        yPos = 800;
                    }
                }

                // Save the report as a PDF file
                report.Save(outputPdfPath);
                Console.WriteLine($"Signature report saved to '{outputPdfPath}'.");
            }
        }
    }
}