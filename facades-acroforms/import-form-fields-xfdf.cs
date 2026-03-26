using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string xfdfFile = "data.xfdf";
        const string outputPdf = "form_filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        using (Form form = new Form(inputPdf, outputPdf))
        {
            using (FileStream xfdfStream = new FileStream(xfdfFile, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfStream);
            }

            form.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
    }
}