using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and signature image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <app> <input-pdf> <signature-image>");
            return;
        }

        string inputPdf = args[0];
        string signatureImage = args[1];

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        // Prepare output file name (e.g., original name with "_signed.pdf")
        string outputPdf = Path.Combine(
            Path.GetDirectoryName(inputPdf) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPdf) + "_signed.pdf");

        try
        {
            // Use PdfFileSignature facade to add a visible signature appearance
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the source PDF
                pdfSign.BindPdf(inputPdf);

                // Set the image that will be shown as the signature appearance
                pdfSign.SignatureAppearance = signatureImage;

                // Define the rectangle where the signature will be placed (in points)
                // System.Drawing.Rectangle is required by the API
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

                // Add a visible signature on page 1.
                // Reason, contact and location are optional strings.
                pdfSign.Sign(
                    page: 1,
                    SigReason: "Signed by application",
                    SigContact: "",
                    SigLocation: "",
                    visible: true,
                    annotRect: rect);

                // Save the signed PDF
                pdfSign.Save(outputPdf);
            }

            Console.WriteLine($"Signed PDF saved to: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}