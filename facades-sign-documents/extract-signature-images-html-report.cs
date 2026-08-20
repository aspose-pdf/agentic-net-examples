using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputHtml = "signature_report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF that contains signatures
            using (var pdfSignature = new Aspose.Pdf.Facades.PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdf);

                // Retrieve all non‑empty signature names
                var signatureNames = pdfSignature.GetSignatureNames();

                var html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Signature Report</title></head><body>");
                html.AppendLine("<h1>Signature Images</h1>");

                foreach (Aspose.Pdf.Facades.SignatureName sigName in signatureNames)
                {
                    // Extract the image (JPEG) for the current signature
                    using (Stream imageStream = pdfSignature.ExtractImage(sigName))
                    {
                        html.AppendLine($"<h2>{sigName}</h2>");

                        if (imageStream != null)
                        {
                            // Convert the image stream to a Base64 string for embedding
                            using (var ms = new MemoryStream())
                            {
                                imageStream.CopyTo(ms);
                                string base64 = Convert.ToBase64String(ms.ToArray());
                                html.AppendLine(
                                    $"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"Signature {sigName}\"/>");
                            }
                        }
                        else
                        {
                            html.AppendLine("<p>No image available for this signature.</p>");
                        }
                    }
                }

                html.AppendLine("</body></html>");

                // Write the HTML report to disk
                File.WriteAllText(outputHtml, html.ToString());
                Console.WriteLine($"Signature report generated: {outputHtml}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}