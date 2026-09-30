using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const int fileCount = 50;
        const string outputPath = "concatenated.pdf";

        // Create 50 small PDF documents in memory (each with a single blank page)
        MemoryStream[] pdfStreams = new MemoryStream[fileCount];
        for (int i = 0; i < fileCount; i++)
        {
            MemoryStream ms = new MemoryStream();
            using (Document doc = new Document())
            {
                doc.Pages.Add();               // add one blank page
                doc.Save(ms);                  // save to memory stream
            }
            ms.Position = 0;                  // reset for reading
            pdfStreams[i] = ms;
        }

        // Concatenate using stream overloads and measure execution time
        using (FileStream outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            Stopwatch sw = Stopwatch.StartNew();

            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(pdfStreams, outputFile); // stream overload

            sw.Stop();
            Console.WriteLine($"Concatenated {fileCount} PDFs in {sw.ElapsedMilliseconds} ms.");
        }

        // Clean up input streams
        foreach (var ms in pdfStreams)
        {
            ms.Dispose();
        }
    }
}