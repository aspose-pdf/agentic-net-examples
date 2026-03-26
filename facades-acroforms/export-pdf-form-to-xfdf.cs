using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "PdfForm.pdf";
        const string outputXfdf = "export.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            Form pdfForm = new Form(inputPdf);
            using (FileStream fs = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportXfdf(fs);
            }
            Console.WriteLine($"Form fields exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
