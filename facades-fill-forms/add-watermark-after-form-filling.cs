using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string filledPath = "filled.pdf";
        const string stampPath = "stamp.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(filledPath))
        {
            Console.Error.WriteLine($"File not found: {filledPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"File not found: {stampPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(filledPath))
            using (PdfFileStamp fileStamp = new PdfFileStamp(doc))
            {
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(stampPath, 1);
                stamp.IsBackground = true;
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPath);
                fileStamp.Close();
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
