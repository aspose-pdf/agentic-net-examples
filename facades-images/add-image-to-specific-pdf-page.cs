using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "image.jpg";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF and image exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // PdfFileMend does not implement IDisposable, so it is instantiated directly.
        PdfFileMend mend = new PdfFileMend();

        // Bind the existing PDF document using a Document instance (required for some overloads).
        using (Document doc = new Document(inputPdf))
        {
            mend.BindPdf(doc);
        }

        // Add the image to page 26 within the rectangle (50,50)-(250,250).
        // The AddImage overload expects an array of page numbers and the rectangle
        // coordinates as separate float values (llx, lly, urx, ury).
        mend.AddImage(imagePath, new int[] { 26 }, 50f, 50f, 250f, 250f);

        // Save the modified PDF to a new file.
        mend.Save(outputPdf);

        Console.WriteLine($"Image added and PDF saved to '{outputPdf}'.");
    }
}
