using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "highlights.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);
            string[] annotTypes = new string[] { "Highlight" };
            int startPage = 1;
            int endPage = editor.Document.Pages.Count;
            using (FileStream fs = File.Create(outputXfdf))
            {
                editor.ExportAnnotationsXfdf(fs, startPage, endPage, annotTypes);
            }
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}