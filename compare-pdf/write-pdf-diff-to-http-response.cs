using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

public static class PdfDiffHelper
{
    // Writes a side‑by‑side PDF diff to the provided output stream.
    // In an ASP.NET Core controller you would pass response.Body as the stream.
    public static void WriteDiff(Stream outputStream, string firstPdfPath, string secondPdfPath)
    {
        if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
            throw new FileNotFoundException("One or both source PDF files were not found.");

        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            var compareOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(doc1, doc2, outputStream, compareOptions);
        }

        // Reset position for callers that will read from the beginning.
        if (outputStream.CanSeek)
            outputStream.Position = 0;
    }
}

// Example console application demonstrating the helper.
class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PdfDiff <firstPdfPath> <secondPdfPath>");
            return;
        }

        string first = args[0];
        string second = args[1];

        using (var ms = new MemoryStream())
        {
            PdfDiffHelper.WriteDiff(ms, first, second);
            // For demo purposes write the diff PDF to a file.
            File.WriteAllBytes("diff.pdf", ms.ToArray());
            Console.WriteLine("Diff PDF saved as diff.pdf");
        }
    }
}