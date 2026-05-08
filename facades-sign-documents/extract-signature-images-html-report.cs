using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";
        const string outputHtml = "signature_report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // StringBuilder to compose the HTML report
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head><meta charset=\"UTF-8\"><title>Signature Report</title></head>");
        html.AppendLine("<body>");
        html.AppendLine("<h1>Signature Images Extracted from PDF</h1>");

        // Use PdfFileSignature facade to work with signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file
            pdfSignature.BindPdf(inputPdf);

            // Retrieve all signature names (true = include empty fields if needed)
            var signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (var sigName in signatureNames)
            {
                // Extract the signature image as a stream (JPEG by default)
                using (Stream imgStream = pdfSignature.ExtractImage(sigName))
                {
                    if (imgStream == null)
                    {
                        // No image found for this signature
                        html.AppendLine($"<h3>{sigName}</h3>");
                        html.AppendLine("<p>No image available.</p>");
                        continue;
                    }

                    // Read the image bytes
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imgStream.CopyTo(ms);
                        byte[] imgBytes = ms.ToArray();

                        // Convert to Base64 for embedding in HTML
                        string base64 = Convert.ToBase64String(imgBytes);

                        // Append image to the HTML report
                        html.AppendLine($"<h3>{sigName}</h3>");
                        html.AppendLine($"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"Signature {sigName}\" style=\"max-width:600px;\"/>");
                    }
                }
            }
        }

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(outputHtml, html.ToString());
        Console.WriteLine($"Signature report generated: {outputHtml}");
    }
}