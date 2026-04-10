using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net;
using Aspose.Pdf.Facades;

class SignatureImageReport
{
    static void Main()
    {
        // Input PDF that contains digital signatures
        const string inputPdfPath = "signed.pdf";
        // Output HTML report file
        const string outputHtmlPath = "signature_report.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdfPath);

        // Retrieve the names of all non‑empty signatures (returns IList<SignatureName>)
        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

        // Build the HTML report
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
        htmlBuilder.AppendLine("<title>Signature Images Report</title>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Signature Images</h1>");

        foreach (SignatureName sigObj in signatureNames)
        {
            string sigName = sigObj.Name; // string representation for display

            // Extract the signature image as a JPEG stream
            Stream imageStream = pdfSignature.ExtractImage(sigObj);

            htmlBuilder.AppendLine($"<h2>{WebUtility.HtmlEncode(sigName)}</h2>");

            if (imageStream != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageStream.CopyTo(ms);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    // Embed the image using a data URI
                    htmlBuilder.AppendLine($"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"Signature {WebUtility.HtmlEncode(sigName)}\"/>");
                }
            }
            else
            {
                htmlBuilder.AppendLine("<p>No image available for this signature.</p>");
            }
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(outputHtmlPath, htmlBuilder.ToString());

        // Clean up the facade
        pdfSignature.Close();

        Console.WriteLine($"Signature image report generated: {outputHtmlPath}");
    }
}
