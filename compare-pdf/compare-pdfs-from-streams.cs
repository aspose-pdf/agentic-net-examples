using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparer
{
    /// <summary>
    /// Compares two PDF documents supplied as streams and returns a PDF containing the visual differences.
    /// The comparison result is written to a memory stream (no temporary files are created).
    /// </summary>
    /// <param name="pdfStream1">First PDF input stream (must be positioned at the beginning).</param>
    /// <param name="pdfStream2">Second PDF input stream (must be positioned at the beginning).</param>
    /// <returns>A MemoryStream containing the diff PDF. Caller is responsible for disposing it.</returns>
    public static MemoryStream ComparePdfStreams(Stream pdfStream1, Stream pdfStream2)
    {
        // Load the source PDFs from the provided streams.
        using (Document doc1 = new Document(pdfStream1))
        using (Document doc2 = new Document(pdfStream2))
        {
            // Create an empty document that will hold the comparison result.
            using (Document resultDoc = new Document())
            {
                // GraphicalPdfComparer performs a pixel‑level visual comparison.
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Compare page by page. The overload that accepts a result Document
                // adds the diff page directly to the provided document.
                int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
                for (int i = 1; i <= pageCount; i++)
                {
                    comparer.ComparePagesToPdf(doc1.Pages[i], doc2.Pages[i], resultDoc);
                }

                // Save the result document into a memory stream.
                MemoryStream outputStream = new MemoryStream();
                resultDoc.Save(outputStream);
                outputStream.Position = 0; // Reset position for the caller to read.
                return outputStream;
            }
        }
    }

    // Entry point required for a console‑type project.
    static void Main(string[] args)
    {
        // The Main method is intentionally left minimal. It can be used for
        // quick manual testing, but the primary API is the ComparePdfStreams method.
        // Example (uncomment to test locally):
        // using (FileStream fs1 = File.OpenRead("first.pdf"))
        // using (FileStream fs2 = File.OpenRead("second.pdf"))
        // {
        //     using (MemoryStream diff = ComparePdfStreams(fs1, fs2))
        //     {
        //         File.WriteAllBytes("diff.pdf", diff.ToArray());
        //     }
        // }
    }
}