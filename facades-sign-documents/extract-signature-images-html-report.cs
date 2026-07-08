using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputHtml = "signature_report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Prepare HTML builder
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html lang=\"en\">");
        htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Signature Images Report</title></head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Signature Images extracted from PDF</h1>");

        // Use PdfFileSignature facade to extract signature images
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF file
            pdfSign.BindPdf(inputPdf);

            // Retrieve all signature names (including empty fields if needed)
            IList<SignatureName> signatureNames = pdfSign.GetSignatureNames(true);

            if (signatureNames == null || signatureNames.Count == 0)
            {
                htmlBuilder.AppendLine("<p>No signatures found in the document.</p>");
            }
            else
            {
                foreach (SignatureName sigInfo in signatureNames)
                {
                    string sigName = sigInfo.Name; // string representation of the signature field

                    // Extract the image for the current signature using the SignatureName overload
                    using (Stream imgStream = pdfSign.ExtractImage(sigInfo))
                    {
                        if (imgStream != null && imgStream.Length > 0)
                        {
                            // Read the image bytes
                            byte[] imgBytes;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                imgStream.CopyTo(ms);
                                imgBytes = ms.ToArray();
                            }

                            // Convert to Base64 for embedding in HTML
                            string base64 = Convert.ToBase64String(imgBytes);
                            // Assume JPEG output (Aspose returns JPEG by default)
                            string imgTag = $"<div><h3>Signature: {sigName}</h3>" +
                                            $"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"{sigName}\" style=\"max-width:600px;\"/>" +
                                            $"</div>";
                            htmlBuilder.AppendLine(imgTag);
                        }
                        else
                        {
                            // No image for this signature
                            htmlBuilder.AppendLine($"<div><h3>Signature: {sigName}</h3><p>No image available.</p></div>");
                        }
                    }
                }
            }
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Save the HTML report
        File.WriteAllText(outputHtml, htmlBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Signature report generated: {outputHtml}");
    }
}
