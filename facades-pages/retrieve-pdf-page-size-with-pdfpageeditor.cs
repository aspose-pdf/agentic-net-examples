using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with PdfPageEditor (facade) and retrieve page 2 size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the document
            Aspose.Pdf.PageSize size = editor.GetPageSize(2); // Pages are 1‑based
            Console.WriteLine($"Page 2 size: Width = {size.Width}, Height = {size.Height}");
        }
    }
}