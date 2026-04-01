using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string htmlReport = "signature-report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        using (Document document = new Document(inputPdf))
        using (PdfFileSignature pdfSignature = new PdfFileSignature(document))
        {
            // GetSignatureNames returns IList<SignatureName>
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head><title>Signature Report</title></head>");
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<h1>Signature Images</h1>");

            foreach (SignatureName sigObj in signatureNames)
            {
                string sigName = sigObj.Name; // the textual name of the signature
                using (Stream imageStream = pdfSignature.ExtractImage(sigObj))
                {
                    if (imageStream != null)
                    {
                        string imageFile = sigName + ".jpg";
                        using (FileStream fileStream = new FileStream(imageFile, FileMode.Create, FileAccess.Write))
                        {
                            imageStream.CopyTo(fileStream);
                        }

                        htmlBuilder.AppendLine($"<h2>{sigName}</h2>");
                        htmlBuilder.AppendLine($"<img src=\"{imageFile}\" alt=\"Signature {sigName}\"/>");
                    }
                    else
                    {
                        htmlBuilder.AppendLine($"<h2>{sigName}</h2>");
                        htmlBuilder.AppendLine("<p>No image found for this signature.</p>");
                    }
                }
            }

            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            File.WriteAllText(htmlReport, htmlBuilder.ToString());
            Console.WriteLine("HTML report generated: " + htmlReport);
        }
    }
}
