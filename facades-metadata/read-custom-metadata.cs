using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string metaKey = "CustomProperty";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfFileInfo fileInfo = new PdfFileInfo())
        {
            fileInfo.BindPdf(inputPath);
            string metaValue = fileInfo.GetMetaInfo(metaKey);

            if (String.IsNullOrEmpty(metaValue))
            {
                Console.WriteLine($"Metadata '{metaKey}' is missing or empty.");
            }
            else
            {
                Console.WriteLine($"Metadata '{metaKey}': {metaValue}");
            }
        }
    }
}