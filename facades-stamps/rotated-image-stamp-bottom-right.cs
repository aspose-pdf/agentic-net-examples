using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine("Stamp image not found: " + stampImagePath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            ImageStamp stamp = new ImageStamp(stampImagePath);
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;
            stamp.RotateAngle = 30.0;

            foreach (Page page in document.Pages)
            {
                page.AddStamp(stamp);
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Stamped PDF saved to '" + outputPath + "'.");
    }
}
