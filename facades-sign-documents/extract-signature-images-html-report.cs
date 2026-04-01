using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string htmlReport = "report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Lists to keep image file names and corresponding signature names
        List<string> imageFiles = new List<string>();
        List<string> signatureNames = new List<string>();

        PdfFileSignature pdfSignature = new PdfFileSignature();
        try
        {
            pdfSignature.BindPdf(inputPdf);

            IList<SignatureName> sigNames = pdfSignature.GetSignatureNames(true);
            int index = 1;
            foreach (SignatureName sigName in sigNames)
            {
                Stream imageStream = pdfSignature.ExtractImage(sigName);
                if (imageStream != null)
                {
                    string imageFile = "sig" + index.ToString() + ".jpg";
                    using (FileStream fileStream = new FileStream(imageFile, FileMode.Create, FileAccess.Write))
                    {
                        imageStream.CopyTo(fileStream);
                    }
                    imageFiles.Add(imageFile);
                    signatureNames.Add(sigName.ToString());
                    index++;
                }
            }
        }
        finally
        {
            pdfSignature.Close();
        }

        // Build a simple HTML report that displays each extracted signature image
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head><title>Signature Images Report</title></head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Signature Images</h1>");

        for (int i = 0; i < imageFiles.Count; i++)
        {
            htmlBuilder.AppendLine("<h3>Signature: " + signatureNames[i] + "</h3>");
            htmlBuilder.AppendLine("<img src=\"" + imageFiles[i] + "\" alt=\"Signature image\" style=\"border:1px solid #000; max-width:600px;\"/>");
            htmlBuilder.AppendLine("<hr/>");
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        File.WriteAllText(htmlReport, htmlBuilder.ToString());

        Console.WriteLine("HTML report generated: " + htmlReport);
    }
}
