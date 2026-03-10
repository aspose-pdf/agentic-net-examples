using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only) and treat it as a PDF document
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Optional: save the converted PDF for verification
            doc.Save(outputPdf); // PDF format, no SaveOptions needed

            // Use the PdfFileInfo facade to read metadata directly
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                Console.WriteLine($"Title: {info.Title}");
                Console.WriteLine($"Author: {info.Author}");
                Console.WriteLine($"Subject: {info.Subject}");
                Console.WriteLine($"Keywords: {info.Keywords}");
                Console.WriteLine($"Creator: {info.Creator}");
                Console.WriteLine($"Producer: {info.Producer}");
                Console.WriteLine($"CreationDate: {info.CreationDate}");
                Console.WriteLine($"ModDate: {info.ModDate}");
                Console.WriteLine($"Number of pages: {info.NumberOfPages}");
                Console.WriteLine($"IsEncrypted: {info.IsEncrypted}");
                Console.WriteLine($"IsPdfFile: {info.IsPdfFile}");
            }
        }
    }
}