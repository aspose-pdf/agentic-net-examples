using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const int pdfCount = 50;
        const string outputPath = "concatenated.pdf";

        // Prepare 50 small PDFs in memory
        List<MemoryStream> sourceStreams = new List<MemoryStream>(pdfCount);
        for (int i = 1; i <= pdfCount; i++)
        {
            // Each PDF is created and disposed inside a using block
            using (Document doc = new Document())
            {
                // Add a simple page with some text
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment($"Document {i}");
                page.Paragraphs.Add(tf);

                // Save to a MemoryStream
                MemoryStream ms = new MemoryStream();
                doc.Save(ms);               // PDF is written to the stream
                ms.Position = 0;            // Reset for reading
                sourceStreams.Add(ms);
            }
        }

        // Prepare the output stream
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate normally
            PdfFileEditor editor = new PdfFileEditor();

            // Measure concatenation time using stream overloads
            Stopwatch sw = Stopwatch.StartNew();
            editor.Concatenate(sourceStreams.ToArray(), outputStream);
            sw.Stop();

            Console.WriteLine($"Concatenated {pdfCount} PDFs in {sw.ElapsedMilliseconds} ms.");

            // Write the concatenated PDF to a file
            outputStream.Position = 0;
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(file);
            }
        }

        // Dispose all source memory streams
        foreach (var ms in sourceStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}