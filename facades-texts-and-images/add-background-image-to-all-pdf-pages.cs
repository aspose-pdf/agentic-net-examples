using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the source PDF to obtain page dimensions and count
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Retrieve page size from the first page (assuming all pages have the same size)
            // MediaBox properties are double, cast to float for PdfFileMend.AddImage
            float pageWidth  = (float)doc.Pages[1].MediaBox.URX;
            float pageHeight = (float)doc.Pages[1].MediaBox.URY;

            // Initialize the facade for modifying the PDF
            PdfFileMend mender = new PdfFileMend();
            mender.BindPdf(inputPdf); // bind the source PDF

            // Add the background image to each page
            for (int i = 1; i <= pageCount; i++)
            {
                // AddImage(string imagePath, int pageNum, float llx, float lly, float urx, float ury)
                // Coordinates cover the whole page (0,0) to (pageWidth,pageHeight)
                mender.AddImage(imagePath, i, 0f, 0f, pageWidth, pageHeight);
            }

            // Save the modified PDF
            mender.Save(outputPdf);
            mender.Close();
        }

        Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdf}'.");
    }
}
