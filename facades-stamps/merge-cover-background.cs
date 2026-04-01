using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath = "cover.pdf";
        const string mainPath = "main.pdf";
        const string outputPath = "merged.pdf";

        if (!System.IO.File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover file not found: {coverPath}");
            return;
        }
        if (!System.IO.File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main file not found: {mainPath}");
            return;
        }

        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.InputFile = mainPath;
        fileStamp.OutputFile = outputPath;

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(coverPath, 1);
        stamp.IsBackground = true;

        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
