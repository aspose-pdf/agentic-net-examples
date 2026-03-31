using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 7 };
            editor.Zoom = 2.0f;
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine("Zoom applied to page 7 and saved to '" + outputPath + "'.");
    }
}