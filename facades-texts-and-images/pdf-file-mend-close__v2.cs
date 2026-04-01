using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        PdfFileMend mend = new PdfFileMend();
        try
        {
            mend.BindPdf(inputPath);
            // Add image to page 1 at coordinates (100,500)-(200,600)
            mend.AddImage(imagePath, 1, 100f, 500f, 200f, 600f);
            mend.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        finally
        {
            mend.Close();
        }
    }
}
