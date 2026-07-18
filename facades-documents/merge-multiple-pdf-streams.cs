using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMerger
{
    /// <summary>
    /// Merges multiple PDF streams into a single PDF stream using Aspose.Pdf.Facades.PdfFileEditor.
    /// </summary>
    /// <param name="inputStreams">Array of input PDF streams. Each stream must be readable.</param>
    /// <param name="outputStream">Writable stream where the merged PDF will be written.</param>
    /// <returns>True if the concatenation succeeded; otherwise, false.</returns>
    public static bool MergePdfStreams(Stream[] inputStreams, Stream outputStream)
    {
        if (inputStreams == null) throw new ArgumentNullException(nameof(inputStreams));
        if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Optional: automatically close the input streams after concatenation.
        editor.CloseConcatenatedStreams = true;

        // Perform concatenation. This overload concatenates an array of streams into a single output stream.
        bool success = editor.Concatenate(inputStreams, outputStream);

        // No need to call any Save method; Concatenate writes directly to the output stream.
        return success;
    }
}

// Entry point required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is intentionally minimal; it only satisfies the compiler.
        // Real usage can be added here or the class can be referenced from another project.
        // Example (commented out):
        // using (FileStream in1 = File.OpenRead("file1.pdf"))
        // using (FileStream in2 = File.OpenRead("file2.pdf"))
        // using (FileStream outStream = File.Create("merged.pdf"))
        // {
        //     bool result = PdfMerger.MergePdfStreams(new Stream[] { in1, in2 }, outStream);
        //     Console.WriteLine($"Merge successful: {result}");
        // }
    }
}