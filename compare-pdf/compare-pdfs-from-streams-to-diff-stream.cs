using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

namespace AsposePdfApi
{
    public static class PdfComparerUtility
    {
        /// <summary>
        /// Compares two PDF documents supplied as in‑memory streams and writes the visual diff
        /// to another memory stream without creating any temporary files.
        /// </summary>
        /// <param name="pdfStream1">First PDF input stream (must be positioned at the beginning).</param>
        /// <param name="pdfStream2">Second PDF input stream (must be positioned at the beginning).</param>
        /// <param name="diffOutputStream">Stream that will receive the resulting diff PDF (will be written from start).</param>
        public static void ComparePdfStreams(Stream pdfStream1, Stream pdfStream2, Stream diffOutputStream)
        {
            // Ensure the input streams are not null.
            if (pdfStream1 == null) throw new ArgumentNullException(nameof(pdfStream1));
            if (pdfStream2 == null) throw new ArgumentNullException(nameof(pdfStream2));
            if (diffOutputStream == null) throw new ArgumentNullException(nameof(diffOutputStream));

            // Load the two source PDFs from the provided streams.
            using (Document doc1 = new Document(pdfStream1))
            using (Document doc2 = new Document(pdfStream2))
            {
                // Create an empty document that will hold the comparison result.
                using (Document resultDoc = new Document())
                {
                    // Instantiate the graphical comparer.
                    GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                    // The number of pages to compare is the minimum of the two documents.
                    int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);

                    // Compare each corresponding page pair and append the diff page to resultDoc.
                    for (int i = 1; i <= pageCount; i++)
                    {
                        Page page1 = doc1.Pages[i];
                        Page page2 = doc2.Pages[i];

                        // The overload that accepts a Document adds the diff page directly to resultDoc.
                        comparer.ComparePagesToPdf(page1, page2, resultDoc);
                    }

                    // Save the resulting diff PDF into the output stream.
                    resultDoc.Save(diffOutputStream);
                }
            }

            // Reset the output stream position so callers can read from the beginning.
            if (diffOutputStream.CanSeek)
            {
                diffOutputStream.Position = 0;
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real library you would change the project output type to "Class Library".
    internal class Program
    {
        static void Main(string[] args)
        {
            // No operation – the method exists only to provide a valid entry point.
            // Example usage (optional):
            // using var stream1 = File.OpenRead("doc1.pdf");
            // using var stream2 = File.OpenRead("doc2.pdf");
            // using var diffStream = new MemoryStream();
            // PdfComparerUtility.ComparePdfStreams(stream1, stream2, diffStream);
        }
    }
}
