using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfMerger
{
    // Merges an array of PDF input streams into a single output stream.
    public static void MergePdfStreams(Stream[] inputStreams, Stream outputStream)
    {
        if (inputStreams == null || inputStreams.Length == 0)
            throw new ArgumentException("No input streams provided.");

        if (outputStream == null)
            throw new ArgumentNullException(nameof(outputStream));

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using.
        var editor = new PdfFileEditor();

        // Ensure each input stream is positioned at the beginning.
        foreach (var s in inputStreams)
        {
            if (s.CanSeek)
                s.Position = 0;
        }

        // Concatenate the PDF streams into the output stream.
        editor.Concatenate(inputStreams, outputStream);

        // Reset output stream position for further reading if needed.
        if (outputStream.CanSeek)
            outputStream.Position = 0;
    }
}

class Program
{
    static void Main()
    {
        // Paths of the PDFs we want to merge.
        string[] pdfPaths = { "first.pdf", "second.pdf", "third.pdf" };

        // Ensure every PDF exists – create an empty placeholder if it does not.
        foreach (var path in pdfPaths)
        {
            if (!File.Exists(path))
            {
                // Create a minimal PDF with a single blank page.
                var placeholder = new Document();
                placeholder.Pages.Add();
                placeholder.Save(path);
                Console.WriteLine($"Placeholder PDF created: {Path.GetFullPath(path)}");
            }
        }

        // Open the PDFs as read‑only streams.
        Stream[] inputs = new Stream[pdfPaths.Length];
        for (int i = 0; i < pdfPaths.Length; i++)
        {
            inputs[i] = File.OpenRead(pdfPaths[i]);
        }

        // Output will be written to a MemoryStream (any writable stream works).
        using (var merged = new MemoryStream())
        {
            PdfMerger.MergePdfStreams(inputs, merged);

            // Optionally save the merged PDF to a file.
            File.WriteAllBytes("merged.pdf", merged.ToArray());
            Console.WriteLine("Merged PDF saved as merged.pdf");
        }

        // Dispose input streams.
        foreach (var s in inputs)
            s.Dispose();
    }
}
