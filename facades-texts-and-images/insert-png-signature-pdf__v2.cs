using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string signatureImage = "signature.png";

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

        // Initialize PdfFileMend and bind the source PDF
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Retrieve the underlying Document to get page count
        Document doc = mend.Document;
        int pageCount = doc.Pages.Count;

        // Evaluation mode allows a maximum of 4 collection elements.
        // The loop below caps the page list at 4 pages to avoid runtime errors.
        int maxPages = Math.Min(pageCount, 4);
        int[] pages = new int[maxPages];
        for (int i = 0; i < maxPages; i++)
        {
            // Aspose.Pdf uses 1‑based page indexing
            pages[i] = i + 1;
        }

        // Bottom‑left corner coordinates (in points). Adjust as needed.
        float lowerLeftX = 0f;
        float lowerLeftY = 0f;
        float upperRightX = 100f;
        float upperRightY = 50f;

        // Add the PNG signature image to the selected pages
        bool added = mend.AddImage(signatureImage, pages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        if (!added)
        {
            Console.Error.WriteLine("Failed to add the signature image.");
        }

        // Save the modified PDF
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"Signature image added. Output saved to '{outputPdf}'.");
    }
}