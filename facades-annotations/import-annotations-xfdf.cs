using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "output.pdf";

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

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            editor.BindPdf(inputPdf);
            editor.ImportAnnotationsFromXfdf(xfdfFile);
            editor.Save(outputPdf);
        }
        finally
        {
            editor.Close();
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}