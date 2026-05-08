using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "edited.pdf";

        // Verify the input file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor implements IDisposable – use a using statement for automatic disposal.
        using (var editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            editor.ReplaceText("Foo", "Bar");
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPdf}'.");
    }
}
