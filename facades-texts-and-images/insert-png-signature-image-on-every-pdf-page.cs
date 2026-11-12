using System;
using System.IO;
using Aspose.Pdf;                 // Document for page count
using Aspose.Pdf.Facades;        // PdfFileMend for image insertion

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed_output.pdf";
        const string signaturePng  = "signature.png";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signaturePng))
        {
            Console.Error.WriteLine($"Signature image not found: {signaturePng}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable – do NOT wrap in using
        PdfFileMend mend = new PdfFileMend();

        // Load the source PDF into the mend object
        mend.BindPdf(inputPdf);

        // Determine the number of pages using Document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;   // Aspose.Pdf uses 1‑based indexing

            // Insert the PNG at the bottom‑left corner of each page
            // Coordinates (0,0) correspond to the lower‑left corner of the page.
            // Width and height are set to 100 and 50 points respectively; adjust as needed.
            for (int i = 1; i <= pageCount; i++)
            {
                // PdfFileMend provides AddImage, not InsertImage
                mend.AddImage(signaturePng, i, 0, 0, 100, 50);
            }
        }

        // Save the modified PDF
        mend.Save(outputPdf);
        // Close releases internal resources (optional but recommended)
        mend.Close();

        Console.WriteLine($"Signature image inserted on all pages. Output saved to '{outputPdf}'.");
    }
}
