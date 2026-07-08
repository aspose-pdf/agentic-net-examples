using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfConcatenator
{
    /// <summary>
    /// Concatenates multiple PDF streams and writes the combined PDF directly to a file.
    /// No temporary files are created; the result is streamed to the output file.
    /// </summary>
    /// <param name="inputStreams">Array of streams, each containing a PDF document.</param>
    /// <param name="outputFilePath">Path of the file where the concatenated PDF will be saved.</param>
    public static void ConcatenatePdfStreams(Stream[] inputStreams, string outputFilePath)
    {
        if (inputStreams == null || inputStreams.Length == 0)
            throw new ArgumentException("At least one input stream must be provided.", nameof(inputStreams));

        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path must be specified.", nameof(outputFilePath));

        // Ensure the output directory exists
        string? outputDir = Path.GetDirectoryName(outputFilePath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create the output file stream (no intermediate storage)
        using (FileStream outputStream = new FileStream(outputFilePath!, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor performs concatenation on streams.
            PdfFileEditor editor = new PdfFileEditor();

            // Close input streams automatically after concatenation to free memory.
            editor.CloseConcatenatedStreams = true;

            // Concatenate all input streams into the output stream.
            editor.Concatenate(inputStreams, outputStream);
        }
    }

    // Helper to create a simple PDF in memory for demo purposes
    private static MemoryStream CreateSamplePdf(string text)
    {
        Document doc = new Document();
        var page = doc.Pages.Add();
        var fragment = new Aspose.Pdf.Text.TextFragment(text);
        page.Paragraphs.Add(fragment);
        MemoryStream ms = new MemoryStream();
        doc.Save(ms);
        ms.Position = 0; // rewind for reading
        return ms;
    }

    // Example usage
    static void Main()
    {
        // Create two PDF memory streams (generated on‑the‑fly, no file I/O required)
        using (MemoryStream ms1 = CreateSamplePdf("First PDF page"))
        using (MemoryStream ms2 = CreateSamplePdf("Second PDF page"))
        {
            Stream[] sources = new Stream[] { ms1, ms2 };
            string resultPath = "merged_output.pdf";

            ConcatenatePdfStreams(sources, resultPath);
            Console.WriteLine($"Merged PDF saved to '{resultPath}'.");
        }
    }
}
