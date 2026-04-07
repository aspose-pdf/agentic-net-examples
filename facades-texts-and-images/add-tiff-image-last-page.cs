using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Image file not found: " + imagePath);
            return;
        }

        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdf);
            int lastPage = mend.Document.Pages.Count;
            // Add the TIFF image to the last page at the desired rectangle
            bool added = mend.AddImage(imagePath, lastPage, 50f, 50f, 200f, 200f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add image to PDF.");
            }
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine("TIFF image added to last page. Saved as " + outputPdf);
    }
}
