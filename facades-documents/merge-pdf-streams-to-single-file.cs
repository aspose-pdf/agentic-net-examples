using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMergeUtility
{
    /// <summary>
    /// Merges multiple PDF streams into a single PDF file saved locally.
    /// No intermediate files are created; the merge is performed directly on streams.
    /// </summary>
    /// <param name="pdfStreams">Enumerable of input PDF streams (e.g., network streams).</param>
    /// <param name="outputFilePath">Full path of the resulting merged PDF file.</param>
    public static void MergePdfStreams(IEnumerable<Stream> pdfStreams, string outputFilePath)
    {
        if (pdfStreams == null) throw new ArgumentNullException(nameof(pdfStreams));
        if (string.IsNullOrWhiteSpace(outputFilePath)) throw new ArgumentException("Output path must be provided.", nameof(outputFilePath));

        // Convert the enumerable to an array as required by PdfFileEditor.Concatenate.
        Stream[] inputArray = ToArray(pdfStreams);

        // Ensure there is at least one source stream.
        if (inputArray.Length == 0)
            throw new ArgumentException("At least one PDF stream must be provided.", nameof(pdfStreams));

        // Create the facade instance (PdfFileEditor does not implement IDisposable).
        PdfFileEditor editor = new PdfFileEditor();

        // Open the output file stream for writing the merged PDF.
        using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
            // Perform the concatenation directly on streams.
            // This overload returns a bool indicating success; we ignore it here but could check if needed.
            editor.Concatenate(inputArray, outputStream);
        }
    }

    // Helper to materialize the IEnumerable<Stream> into a Stream[].
    private static Stream[] ToArray(IEnumerable<Stream> streams)
    {
        List<Stream> list = new List<Stream>();
        foreach (Stream s in streams)
        {
            if (s == null) throw new ArgumentException("One of the input streams is null.", nameof(streams));
            list.Add(s);
        }
        return list.ToArray();
    }
}

// Dummy entry point so the project compiles as a console application.
public class Program
{
    public static void Main(string[] args)
    {
        // The utility does not require a console entry point for its core functionality.
        // This placeholder satisfies the compiler requirement for a static Main method.
        // Example (commented out) of how the utility could be invoked:
        // var pdfStreams = new List<Stream> { File.OpenRead("file1.pdf"), File.OpenRead("file2.pdf") };
        // PdfMergeUtility.MergePdfStreams(pdfStreams, "merged.pdf");
    }
}