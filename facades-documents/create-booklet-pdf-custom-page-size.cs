using System;
using System.IO;
using Aspose.Pdf;               // Needed for PageSize and PageInfo
using Aspose.Pdf.Facades;      // PdfFileEditor

class BookletCreator
{
    static void Main()
    {
        // Paths to the source PDF and the resulting booklet PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        bool bookletCreated;

        // Create the booklet using streams (no PageSize overload for streams)
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bookletCreated = editor.MakeBooklet(inputStream, outputStream);
        }

        if (!bookletCreated)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        // Set custom page dimensions (e.g., A5) on the generated booklet
        // This must be done after the streams are closed, so we reopen the file.
        using (Document bookletDoc = new Document(outputPath))
        {
            // Use the PageSize class to obtain standard dimensions in points
            var customSize = PageSize.A5; // Width = 420, Height = 595 (portrait)

            foreach (Page page in bookletDoc.Pages)
            {
                page.PageInfo.Width = customSize.Width;
                page.PageInfo.Height = customSize.Height;
                page.PageInfo.IsLandscape = false; // portrait; set true for landscape
            }

            bookletDoc.Save(outputPath);
        }

        Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
    }
}
