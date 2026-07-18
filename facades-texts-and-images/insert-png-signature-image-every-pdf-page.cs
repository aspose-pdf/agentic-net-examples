using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed_output.pdf";
        const string signaturePng  = "signature.png";

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

        // Load the PDF to obtain the page count (uses the standard Document lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Build an array of all page numbers (Aspose.Pdf uses 1‑based indexing)
            int[] allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                allPages[i] = i + 1;

            // Create the PdfFileMend facade and bind the source PDF
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(inputPdf);

            // Define the rectangle for the signature image at the bottom‑left corner.
            // lowerLeftX/Y = 0 places the image at the origin of the page.
            // upperRightX/Y define the image size (e.g., 100 pt width, 50 pt height).
            float lowerLeftX   = 0f;
            float lowerLeftY   = 0f;
            float upperRightX  = 100f; // width of the signature
            float upperRightY  = 50f;  // height of the signature

            // Add the PNG image to every page in a single call.
            mend.AddImage(signaturePng, allPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified PDF.
            mend.Save(outputPdf);
            mend.Close(); // optional; releases resources
        }

        Console.WriteLine($"Signature image added to every page → '{outputPdf}'");
    }
}