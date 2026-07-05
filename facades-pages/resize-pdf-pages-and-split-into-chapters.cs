using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // source PDF
        const string resizedPath = "resized.pdf";           // temporary resized PDF
        const string outputFolder = "Chapters";             // folder for chapter PDFs
        string outputTemplate = Path.Combine(outputFolder, "chapter_%NUM%.pdf"); // %NUM% will be replaced by page number

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Determine all page numbers (Aspose.Pdf uses 1‑based indexing)
        int[] allPages;
        using (Document doc = new Document(inputPath))
        {
            allPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
        }

        // Resize the contents of every page uniformly.
        // ResizeContentsPct shrinks the page contents by the given percentages.
        // Here we shrink both width and height to 90 % of the original size.
        PdfFileEditor editor = new PdfFileEditor();
        double widthPercent = 90.0;   // 90 % of original width
        double heightPercent = 90.0;  // 90 % of original height
        editor.ResizeContentsPct(inputPath, resizedPath, allPages, widthPercent, heightPercent);

        // Split the resized PDF into separate files.
        // SplitToPages creates one PDF per page and substitutes %NUM% with the page number.
        editor.SplitToPages(resizedPath, outputTemplate);

        Console.WriteLine("PDF has been resized and split into individual chapter files.");
    }
}