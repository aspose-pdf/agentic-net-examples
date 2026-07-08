using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

public static class PdfComparer
{
    /// <summary>
    /// Compares two PDF documents supplied as in‑memory streams and returns a PDF stream
    /// that contains the graphical differences. No temporary files are created.
    /// </summary>
    /// <param name="pdfStream1">First PDF stream (must be positioned at the start).</param>
    /// <param name="pdfStream2">Second PDF stream (must be positioned at the start).</param>
    /// <returns>A MemoryStream containing the diff PDF.</returns>
    public static MemoryStream ComparePdfStreams(MemoryStream pdfStream1, MemoryStream pdfStream2)
    {
        // Ensure the input streams are at the beginning
        pdfStream1.Position = 0;
        pdfStream2.Position = 0;

        // Load the PDFs directly from the streams using the Document constructor
        using (Document doc1 = new Document(pdfStream1))
        using (Document doc2 = new Document(pdfStream2))
        using (Document diffDoc = new Document())
        {
            // GraphicalPdfComparer can write the diff directly into a Document instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Compare page by page (up to the shorter document length)
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            for (int i = 1; i <= pageCount; i++)
            {
                comparer.ComparePagesToPdf(doc1.Pages[i], doc2.Pages[i], diffDoc);
            }

            // Save the result into a memory stream
            MemoryStream resultStream = new MemoryStream();
            diffDoc.Save(resultStream);
            resultStream.Position = 0; // Reset for reading by the caller
            return resultStream;
        }
    }
}

// Dummy entry point – required when the project type is a console application.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank. The library method can be called from other code.
    }
}