using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfBookmarkHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, adds a bookmark that points to an external URL,
    /// and returns the modified PDF as a new byte array.
    /// </summary>
    /// <param name="pdfBytes">The original PDF content.</param>
    /// <returns>Byte array containing the updated PDF.</returns>
    public static byte[] UpdatePdfBookmarks(byte[] pdfBytes)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

        // Load the PDF from the input memory stream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Create a new bookmark (outline item) that navigates to an external URL.
            Aspose.Pdf.OutlineItemCollection bookmark = new Aspose.Pdf.OutlineItemCollection(doc.Outlines)
            {
                Title = "Example Site",
                Action = new GoToURIAction("https://example.org")
            };

            // Add the bookmark to the document's outline collection.
            doc.Outlines.Add(bookmark);

            // Save the modified PDF into an output memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point required for a console‑type project.
    /// Demonstrates how to call <see cref="PdfBookmarkHelper.UpdatePdfBookmarks"/>.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <exe> <input-pdf-path>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        byte[] originalPdf = File.ReadAllBytes(inputPath);
        byte[] updatedPdf = PdfBookmarkHelper.UpdatePdfBookmarks(originalPdf);

        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                         Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf");
        File.WriteAllBytes(outputPath, updatedPdf);
        Console.WriteLine($"Updated PDF saved to: {outputPath}");
    }
}
