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

        // Load the source PDF into a Document instance.
        using (Document doc = new Document(inputPdf))
        {
            // PdfFileMend does not implement IDisposable, instantiate directly.
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);

            // Add the image to page 2 at coordinates (100,150).
            // The overload requires an int[] of page numbers and explicit width/height.
            // Here we use a 200x200 size – adjust as needed.
            mend.AddImage(imagePath, new int[] { 2 }, 100f, 150f, 200f, 200f);

            // Save the modified PDF.
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image added and saved to '{outputPdf}'.");
    }
}
