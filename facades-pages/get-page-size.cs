using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);
        PageSize size = editor.GetPageSize(2);
        Console.WriteLine("Size of page 2 : " + size.Width + " x " + size.Height);
        editor.Close();
    }
}