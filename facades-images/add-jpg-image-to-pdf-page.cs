using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
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

        // PdfFileMend does not implement IDisposable, so we instantiate it directly.
        PdfFileMend mend = new PdfFileMend();

        // Bind the existing PDF document to the Mend object using a Document instance.
        using (Document doc = new Document(inputPdf))
        {
            mend.BindPdf(doc);

            // Add the JPG image to page 2.
            // The AddImage overload expects an int[] of page numbers and a rectangle (left, bottom, right, top).
            // Here we place the image with its lower‑left corner at (100,200) and a simple size.
            mend.AddImage(imagePath, new int[] { 2 }, 100f, 200f, 500f, 700f);

            // Save the modified PDF to the specified output path.
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image added and PDF saved to '{outputPdf}'.");
    }
}
