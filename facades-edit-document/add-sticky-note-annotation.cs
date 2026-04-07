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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20);
            string title = "User Comment";
            string contents = "This is a sticky note annotation.";
            bool open = false;
            string icon = "Note";
            int pageNumber = 1;
            editor.CreateText(rect, title, contents, open, icon, pageNumber);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}