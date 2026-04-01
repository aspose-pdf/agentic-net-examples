using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "image.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        long originalSize = new FileInfo(inputPdf).Length;
        Console.WriteLine($"Original PDF size: {originalSize} bytes");

        using (Document doc = new Document(inputPdf))
        {
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);
            // Add image to page 1 at coordinates (100,500) – (300,650)
            mend.AddImage(imagePath, new int[] { 1 }, 100f, 500f, 300f, 650f);
            mend.Save(outputPdf);
        }

        long newSize = new FileInfo(outputPdf).Length;
        Console.WriteLine($"Modified PDF size: {newSize} bytes");

        if (newSize > originalSize)
        {
            Console.WriteLine("PASS: PDF size increased after adding image.");
        }
        else
        {
            Console.WriteLine("FAIL: PDF size did not increase as expected.");
        }
    }
}
