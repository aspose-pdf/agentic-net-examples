using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string bookletPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Resize each page to landscape orientation
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double width  = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                // Swap dimensions if the page is portrait
                if (height > width)
                {
                    page.PageInfo.Width  = height;
                    page.PageInfo.Height = width;
                }

                // Explicitly mark the page as landscape (optional, helps some APIs)
                page.PageInfo.IsLandscape = true;
            }

            // Save the resized PDF (temporary file)
            doc.Save(resizedPath);
        }

        // Generate a booklet from the resized PDF using PdfFileEditor
        // PdfFileEditor does not implement IDisposable, so we just instantiate it
        PdfFileEditor editor = new PdfFileEditor();

        // Use the MakeBooklet overload that does not require a PageSize argument
        bool success = editor.MakeBooklet(resizedPath, bookletPath);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        Console.WriteLine($"Booklet created at '{bookletPath}'.");
    }
}
