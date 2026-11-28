using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "imageRich.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePath}");
            return;
        }

        // PdfFileMend does not implement IDisposable, so no using block is needed
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF
        mend.BindPdf(sourcePath);

        // Insert images on different pages
        InsertImageIfExists(mend, "image1.jpg", 1, 100, 500, 300, 700);
        InsertImageIfExists(mend, "image2.png", 2, 50, 400, 250, 600);
        InsertImageIfExists(mend, "image3.tif", 3, 150, 200, 350, 400);

        // Save the modified PDF
        mend.Save(outputPath);

        Console.WriteLine($"Images inserted and saved to '{outputPath}'.");
    }

    static void InsertImageIfExists(PdfFileMend mend, string imagePath, int pageNumber,
                                    double llx, double lly, double urx, double ury)
    {
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath} (skipped)");
            return;
        }

        // PdfFileMend.AddImage expects a Stream for the image, a single int page number,
        // and float coordinates.
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            mend.AddImage(
                imgStream,
                pageNumber,
                (float)llx,
                (float)lly,
                (float)urx,
                (float)ury);
        }
    }
}
