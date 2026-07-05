using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // 1. Create the booklet using streams (no PageSize overload).
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.MakeBooklet(inputStream, outputStream);
        }

        // 2. Apply custom page dimensions (e.g., A5) to the generated booklet.
        using (Document bookletDoc = new Document(outputPath))
        {
            foreach (Page page in bookletDoc.Pages)
            {
                page.PageInfo.Width = PageSize.A5.Width;   // set width in points
                page.PageInfo.Height = PageSize.A5.Height; // set height in points
                page.PageInfo.IsLandscape = false;        // optional: ensure portrait orientation
            }
            bookletDoc.Save(outputPath);
        }

        Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
    }
}
