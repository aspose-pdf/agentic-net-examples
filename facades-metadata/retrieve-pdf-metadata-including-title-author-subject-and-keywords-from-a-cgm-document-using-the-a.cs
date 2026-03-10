using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file and convert it to a PDF Document in memory
        using (Document pdfDoc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Use PdfFileInfo facade to read metadata from the PDF document
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                Console.WriteLine($"Title   : {info.Title}");
                Console.WriteLine($"Author  : {info.Author}");
                Console.WriteLine($"Subject : {info.Subject}");
                Console.WriteLine($"Keywords: {info.Keywords}");
            }
        }
    }
}
