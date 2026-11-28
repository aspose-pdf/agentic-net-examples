using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "DemoForm.pdf";
        const string fdfPath = "FormData.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Bind the PDF form and export its data to FDF
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath);

            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
                // fdfStream is closed automatically by the using block
            }
        }

        Console.WriteLine($"Form data exported to '{fdfPath}'.");
    }
}