using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");
            if (String.IsNullOrEmpty(reviewedBy))
            {
                Console.WriteLine("ReviewedBy metadata not found.");
            }
            else
            {
                Console.WriteLine($"ReviewedBy: {reviewedBy}");
            }
        }
    }
}