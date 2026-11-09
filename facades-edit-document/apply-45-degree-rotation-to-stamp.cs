using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the source PDF to obtain the page count (PdfFileStamp no longer exposes GetPageCount)
        Document srcDoc = new Document(inputPdfPath);
        int pageCount = srcDoc.Pages.Count;

        // Bind the PDF to the stamp processor
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(inputPdfPath);

            // Create a stamp based on the image and set rotation
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImagePath);
            stamp.Rotation = 45f;               // rotate 45 degrees for diagonal placement
            stamp.IsBackground = false;        // overlay on top of existing content
            stamp.SetOrigin(100f, 100f);        // position (adjust as needed)

            // Apply the stamp to every page
            stamp.Pages = Enumerable.Range(1, pageCount).ToArray();
            pdfStamp.AddStamp(stamp);

            // Save the modified PDF
            pdfStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Rotated stamp applied. Output saved to '{outputPdfPath}'.");
    }
}
