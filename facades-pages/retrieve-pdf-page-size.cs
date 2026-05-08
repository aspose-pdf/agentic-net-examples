using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Ensure a PDF with at least two pages exists before we try to read it
        if (!System.IO.File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // Bind the PDF to the editor and retrieve the size of page 2
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Pages are 1‑based; get size of the second page
            PageSize size = editor.GetPageSize(2);

            // Log the dimensions
            Console.WriteLine($"Page 2 size: Width = {size.Width}, Height = {size.Height}");
        }
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a simple PDF with two blank pages (default A4 size)
        Document doc = new Document();
        doc.Pages.Add(); // page 1
        doc.Pages.Add(); // page 2
        doc.Save(path);
    }
}