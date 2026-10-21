using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Simple console usage: PdfCleaner <input.pdf> [output.pdf]
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfCleaner <input.pdf> [output.pdf]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? ".", "cleaned.pdf");

        // Open the source PDF, clean it, and write the result.
        using (FileStream inputFile = File.OpenRead(inputPath))
        using (MemoryStream cleanedStream = CleanPdf(inputFile))
        using (FileStream outputFile = File.Create(outputPath))
        {
            cleanedStream.CopyTo(outputFile);
        }

        Console.WriteLine($"Cleaned PDF saved to {outputPath}");
    }

    /// <summary>
    /// Removes all annotations from a PDF stream and returns a new stream containing the cleaned PDF.
    /// </summary>
    /// <param name="pdfStream">A readable stream that contains the original PDF.</param>
    /// <returns>A MemoryStream positioned at the beginning, holding the cleaned PDF.</returns>
    public static MemoryStream CleanPdf(Stream pdfStream)
    {
        // Aspose.Pdf.Facades requires a seek‑able stream, so copy the incoming stream into a MemoryStream.
        MemoryStream inputMemory = new MemoryStream();
        pdfStream.CopyTo(inputMemory);
        inputMemory.Position = 0;

        // PdfAnnotationEditor implements IDisposable, so use a using block.
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            annotationEditor.BindPdf(inputMemory);

            // Delete every annotation in the document. The Facade provides DeleteAnnotations() for this purpose.
            annotationEditor.DeleteAnnotations();

            // Optionally flatten any remaining annotation appearance (not required for pure removal).
            // annotationEditor.FlatteningAnnotations();

            // Save the cleaned document into a new MemoryStream.
            MemoryStream outputMemory = new MemoryStream();
            annotationEditor.Save(outputMemory);
            outputMemory.Position = 0; // Reset for the caller.
            return outputMemory;
        }
    }
}
