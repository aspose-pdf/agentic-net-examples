using System;
using System.IO;
using System.Collections.Generic;
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

        // Collect HTML fragments for each signature image
        List<string> imageHtmlBlocks = new List<string>();

        // Use PdfFileSignature facade to work with signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file
            pdfSignature.BindPdf(inputPdf);

            // Get all signature names (returns IList<SignatureName>)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (SignatureName sigInfo in signatureNames)
            {
                // Extract the signature image using the SignatureName overload
                using (Stream imgStream = pdfSignature.ExtractImage(sigInfo))
                {
                    if (imgStream == null)
                    {
                        // No image for this signature; skip
                        continue;
                    }

                    // Read the stream into a byte array
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imgStream.CopyTo(ms);
                        byte[] imgBytes = ms.ToArray();

                        // Convert to Base64 for embedding in HTML
                        string base64 = Convert.ToBase64String(imgBytes);

                        // Build an <img> tag with data URI
                        string imgTag = $"<div><h3>Signature: {sigInfo.Name}</h3>" +
                                        $"<img src=\"data:image/jpeg;base64,{base64}\" " +
                                        $"style=\"border:1px solid #ccc; max-width:600px;\" />" +
                                        $"</div>";

                        imageHtmlBlocks.Add(imgTag);
                    }
                }
            }
        }

        // Assemble final HTML report
        string htmlContent = "<!DOCTYPE html>\n<html>\n<head>\n" +
                             "<meta charset=\"UTF-8\">\n" +
                             "<title>Signature Image Report</title>\n" +
                             "<style>body {font-family:Arial,Helvetica,sans-serif; margin:20px;}</style>\n" +
                             "</head>\n<body>\n" +
                             "<h1>Signature Images Extracted from PDF</h1>\n";

        if (imageHtmlBlocks.Count == 0)
        {
            htmlContent += "<p>No signature images were found in the document.</p>\n";
        }
        else
        {
            foreach (string block in imageHtmlBlocks)
            {
                htmlContent += block + "\n";
            }
        }

        htmlContent += "</body>\n</html>";

        // Write the HTML file
        File.WriteAllText(outputHtml, htmlContent);
        Console.WriteLine($"Signature report generated: {outputHtml}");
    }
}
