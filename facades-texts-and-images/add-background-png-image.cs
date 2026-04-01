using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string backgroundImagePath = "background.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Load the document to obtain the page count.
        using (Document doc = new Document(inputPdfPath))
        {
            int pageCount = doc.Pages.Count;

            // Initialize PdfFileMend with source and destination files.
            PdfFileMend mend = new PdfFileMend(inputPdfPath, outputPdfPath);

            // Iterate through all pages and add the background image.
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // AddImage(string imagePath, int pageNum, float llx, float lly, float urx, float ury)
                // Here we place the image at the lower‑left corner (0,0) with a size of 200x200 points.
                mend.AddImage(backgroundImagePath, pageNumber, 0f, 0f, 200f, 200f);
            }

            // Finalize the operation.
            mend.Close();
        }

        Console.WriteLine($"Background image added to all pages. Saved as '{outputPdfPath}'.");
    }
}