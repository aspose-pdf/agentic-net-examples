using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string signatureImage = "signature.png";

        // Verify that the source PDF and the PNG image exist.
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

        // PdfFileMend implements IDisposable, so wrap it in a using block.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Load the PDF file into the facade.
            mend.BindPdf(inputPdf);

            // Aspose.Pdf uses 1‑based page indexing.
            int pageCount = mend.Document.Pages.Count;

            // Add the PNG to every page.
            // Coordinates are in default PDF points (1/72 inch).
            // Here we place the image at the bottom‑left corner (0,0) with a size of 100×100 points.
            for (int page = 1; page <= pageCount; page++)
            {
                using (FileStream imgStream = File.OpenRead(signatureImage))
                {
                    mend.AddImage(imgStream, page, 0f, 0f, 100f, 100f);
                }
            }

            // Persist the changes to a new file.
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Signature image added to all pages. Output saved to '{outputPdf}'.");
    }
}