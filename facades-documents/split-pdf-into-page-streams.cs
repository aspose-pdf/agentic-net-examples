using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the source PDF with Document (PdfFileEditor does not provide SplitToIndividual)
        Document sourceDoc = new Document(inputPath);
        int pageCount = sourceDoc.Pages.Count;

        // Prepare an array of MemoryStream objects – one for each page
        MemoryStream[] pageStreams = new MemoryStream[pageCount];

        for (int i = 0; i < pageCount; i++)
        {
            // Create a new Document that will contain only the current page
            Document singlePageDoc = new Document();
            // Copy the page from the source document (Pages collection is 1‑based)
            singlePageDoc.Pages.Add(sourceDoc.Pages[i + 1]);

            // Save the single‑page document into a MemoryStream
            MemoryStream ms = new MemoryStream();
            singlePageDoc.Save(ms, SaveFormat.Pdf);
            ms.Position = 0; // reset position for later reading
            pageStreams[i] = ms;
        }

        // Optional: save each MemoryStream to a separate PDF file for verification
        for (int i = 0; i < pageStreams.Length; i++)
        {
            string outputPath = $"page_{i + 1}.pdf";
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].CopyTo(file);
            }
            pageStreams[i].Dispose();
        }

        Console.WriteLine("PDF successfully split into individual page streams.");
    }
}
