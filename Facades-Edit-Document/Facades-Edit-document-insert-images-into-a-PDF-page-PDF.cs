using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF path
        // 1 - image file path to insert
        // 2 - target page number (1‑based)
        // 3 - lower‑left X coordinate (float)
        // 4 - lower‑left Y coordinate (float)
        // 5 - upper‑right X coordinate (float)
        // 6 - upper‑right Y coordinate (float)
        // 7 - output PDF path
        if (args.Length < 8)
        {
            Console.WriteLine("Usage: <inputPdf> <imagePath> <pageNumber> <llx> <lly> <urx> <ury> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string imagePath = args[1];
        int pageNumber = int.Parse(args[2]);
        float llx = float.Parse(args[3]);
        float lly = float.Parse(args[4]);
        float urx = float.Parse(args[5]);
        float ury = float.Parse(args[6]);
        string outputPdfPath = args[7];

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file not found at '{imagePath}'.");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Use PdfFileMend (Facades API) to insert the image
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the PDF document to the editor
            mend.BindPdf(pdfDocument);

            // Open the image as a stream (required overload)
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // Add the image to the specified page and rectangle
                mend.AddImage(imgStream, pageNumber, llx, lly, urx, ury);
            }
        }

        // Save the modified PDF (using the provided document-save rule)
        pdfDocument.Save(outputPdfPath);
    }
}