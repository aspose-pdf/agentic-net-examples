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
        const string inputPath = "input.pdf";
        const string outputPath = "report.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Collect signature information
        List<string> lines = new List<string>();
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();
            lines.Add("Signature Report");
            lines.Add("================");
            lines.Add("");

            if (signatureNames.Count == 0)
            {
                lines.Add("No signatures found in the document.");
            }
            else
            {
                for (int i = 0; i < signatureNames.Count; i++)
                {
                    SignatureName sigName = signatureNames[i];
                    string signer = pdfSignature.GetSignerName(sigName);
                    bool isValid = pdfSignature.VerifySignature(sigName);
                    string reason = pdfSignature.GetReason(sigName);
                    string location = pdfSignature.GetLocation(sigName);
                    DateTime dateTime = pdfSignature.GetDateTime(sigName);
                    int revision = pdfSignature.GetRevision(sigName);
                    bool coversWhole = pdfSignature.CoversWholeDocument(sigName);

                    lines.Add($"Signature {i + 1}: {sigName}");
                    lines.Add($"  Signer: {signer}");
                    lines.Add($"  Valid: {isValid}");
                    lines.Add($"  Reason: {reason}");
                    lines.Add($"  Location: {location}");
                    lines.Add($"  Date/Time: {dateTime}");
                    lines.Add($"  Revision: {revision}");
                    lines.Add($"  Covers whole document: {coversWhole}");
                    lines.Add("");
                }
                int totalRevision = pdfSignature.GetTotalRevision();
                lines.Add($"Total document revisions: {totalRevision}");
            }
        }

        // Create PDF report
        using (Document reportDoc = new Document())
        {
            Page page = reportDoc.Pages.Add();
            // PageInfo.Height is double; cast to float for arithmetic with float literals
            float verticalPosition = (float)page.PageInfo.Height - 50f;
            foreach (string line in lines)
            {
                TextFragment fragment = new TextFragment(line);
                fragment.Position = new Position(50f, verticalPosition);
                page.Paragraphs.Add(fragment);
                verticalPosition -= 15f;
                if (verticalPosition < 50f)
                {
                    page = reportDoc.Pages.Add();
                    verticalPosition = (float)page.PageInfo.Height - 50f;
                }
            }
            reportDoc.Save(outputPath);
        }

        Console.WriteLine("Signature report saved to '" + outputPath + "'.");
    }
}
