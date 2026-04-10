using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the background image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the PDF to obtain the page count
        int[] pageNumbers;
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;
            pageNumbers = Enumerable.Range(1, pageCount).ToArray();
        }

        // Create a PdfFileMend facade, specify input and output files
        using (PdfFileMend mender = new PdfFileMend(inputPdf, outputPdf))
        {
            // Add the background image to all pages.
            // Coordinates (0,0) lower‑left to (595,842) upper‑right cover a typical A4 page.
            mender.AddImage(imagePath, pageNumbers, 0f, 0f, 595f, 842f);
            // Close the facade to finalize the output file
            mender.Close();
        }

        Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdf}'.");
    }
}