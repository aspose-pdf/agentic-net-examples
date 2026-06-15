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

        try
        {
            // Initialize the facade and bind the PDF
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdf);

                // Retrieve all signature names (including empty ones if desired)
                var signatureNames = pdfSignature.GetSignatureNames(true);

                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Signature Report</title></head><body>");
                htmlBuilder.AppendLine("<h1>Signature Images Report</h1>");

                foreach (var sigName in signatureNames)
                {
                    // Extract the image stream for the current signature
                    using (Stream imgStream = pdfSignature.ExtractImage(sigName))
                    {
                        if (imgStream == null)
                        {
                            // No image for this signature – skip
                            continue;
                        }

                        // Read the image into a memory buffer
                        using (MemoryStream ms = new MemoryStream())
                        {
                            imgStream.CopyTo(ms);
                            string base64 = Convert.ToBase64String(ms.ToArray());

                            // Embed the image as a data‑URL in the HTML
                            htmlBuilder.AppendLine($"<h3>Signature: {sigName}</h3>");
                            htmlBuilder.AppendLine(
                                $"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"Signature {sigName}\" style=\"max-width:600px;\"/>");
                        }
                    }
                }

                htmlBuilder.AppendLine("</body></html>");

                // Write the HTML report to disk
                File.WriteAllText(outputHtml, htmlBuilder.ToString());
                Console.WriteLine($"Signature report generated: {outputHtml}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}