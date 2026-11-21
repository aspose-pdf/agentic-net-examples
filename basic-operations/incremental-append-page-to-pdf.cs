using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string originalPdf = "original.pdf";
        const string pagesToAdd = "newpages.pdf";
        const string outputPdf = "updated.pdf";

        if (!File.Exists(originalPdf) || !File.Exists(pagesToAdd))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // Create the output file as a copy of the original so that we can perform an incremental update.
        File.Copy(originalPdf, outputPdf, true);

        // Open the output PDF with a read/write FileStream. Loading the document from this stream
        // allows Aspose.Pdf to perform an incremental update when Save() is called without options.
        using (FileStream outStream = new FileStream(outputPdf, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(outStream))
        using (Document src = new Document(pagesToAdd))
        {
            // Append all pages from the source document.
            doc.Pages.Add(src.Pages);

            // Parameterless Save performs an incremental update on the opened stream.
            doc.Save();
        }

        Console.WriteLine($"PDF saved with incremental update to '{outputPdf}'.");
    }
}
