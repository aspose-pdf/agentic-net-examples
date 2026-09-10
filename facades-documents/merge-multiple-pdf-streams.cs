using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    // Creates a simple one‑page PDF and returns it as a MemoryStream.
    private static MemoryStream CreateSamplePdf(string title)
    {
        var doc = new Document();
        var page = doc.Pages.Add();
        // Add a simple text paragraph so the PDF is not empty.
        page.Paragraphs.Add(new TextFragment(title));
        var ms = new MemoryStream();
        doc.Save(ms);
        ms.Position = 0; // reset for reading
        return ms;
    }

    // Merges multiple PDF streams into a single PDF stream using PdfFileEditor.
    static void MergePdfStreams(Stream[] inputStreams, Stream outputStream)
    {
        // PdfFileEditor does NOT implement IDisposable; instantiate directly.
        var editor = new PdfFileEditor();
        // Automatically close the input streams after concatenation.
        editor.CloseConcatenatedStreams = true;
        // Perform concatenation. Returns true if successful.
        bool success = editor.Concatenate(inputStreams, outputStream);
        if (!success)
        {
            throw new InvalidOperationException("PDF concatenation failed.");
        }
        // No need to call any Save method; Concatenate writes directly to outputStream.
    }

    static void Main()
    {
        // Create three sample PDFs in memory.
        var sampleStreams = new[]
        {
            CreateSamplePdf("Sample PDF 1"),
            CreateSamplePdf("Sample PDF 2"),
            CreateSamplePdf("Sample PDF 3")
        };

        // Output file path for the merged PDF.
        const string outputFile = "merged.pdf";

        // Merge the PDFs into a file.
        using (var outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            MergePdfStreams(sampleStreams, outStream);
        }

        // Input streams are closed automatically because CloseConcatenatedStreams = true.
        // No explicit disposal required, but disposing is safe.
        foreach (var s in sampleStreams)
        {
            s.Dispose();
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}
