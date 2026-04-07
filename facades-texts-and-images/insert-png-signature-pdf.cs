using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string signatureImage = "signature.png";
        const string outputPdf = "signed.pdf";

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

        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdf);

            Document doc = mend.Document;
            int pageCount = doc.Pages.Count;
            int[] allPages = Enumerable.Range(1, pageCount).ToArray();

            // Bottom‑left corner rectangle (example size 100x50 points)
            float lowerLeftX = 10f;
            float lowerLeftY = 10f;
            float upperRightX = 110f;
            float upperRightY = 60f;

            bool added = mend.AddImage(signatureImage, allPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add signature image to the PDF.");
            }

            mend.Save(outputPdf);
        }

        Console.WriteLine($"Signature added to all pages. Output saved as '{outputPdf}'.");
    }
}