using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Determine the number of pages in the source PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Prepare an array of MemoryStream objects – one per page
        MemoryStream[] pageStreams = new MemoryStream[pageCount];

        // Split the PDF into individual pages, each written to its corresponding MemoryStream
        for (int i = 0; i < pageCount; i++)
        {
            // Load the source document once per iteration (could be optimized by re‑using the original Document)
            using (Document srcDoc = new Document(inputPath))
            {
                // Create a new document that will contain only the current page
                Document singlePageDoc = new Document();
                // Pages are 1‑based in Aspose.Pdf
                singlePageDoc.Pages.Add(srcDoc.Pages[i + 1]);

                MemoryStream ms = new MemoryStream();
                // Save the single‑page document into the memory stream
                singlePageDoc.Save(ms, SaveFormat.Pdf);
                // Reset position so callers can read from the beginning
                ms.Position = 0;
                pageStreams[i] = ms;
            }
        }

        // Optional: save each MemoryStream to a separate file for verification
        for (int i = 0; i < pageCount; i++)
        {
            string outPath = $"page_{i + 1}.pdf";
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].CopyTo(fs);
            }
        }

        // Clean up the MemoryStream instances
        foreach (var ms in pageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine("PDF split into individual page MemoryStreams successfully.");
    }
}
