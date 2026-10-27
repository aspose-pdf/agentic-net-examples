using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string metaKey = "ProjectCode";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo allows reading metadata without loading the full PDF.
        PdfFileInfo fileInfo = new PdfFileInfo(pdfPath);
        string metaValue = fileInfo.GetMetaInfo(metaKey);

        if (metaValue != null)
            Console.WriteLine($"{metaKey}: {metaValue}");
        else
            Console.WriteLine($"Metadata '{metaKey}' not found.");
    }
}