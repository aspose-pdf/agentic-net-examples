using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class PdfComparisonInMemory
{
    /// <summary>
    /// Compares two PDF documents supplied as streams and returns the diff PDF as a stream.
    /// </summary>
    /// <param name="pdfStream1">First PDF input stream (must be readable and seekable).</param>
    /// <param name="pdfStream2">Second PDF input stream.</param>
    /// <returns>A MemoryStream containing the comparison result PDF.</returns>
    public static MemoryStream ComparePdfs(Stream pdfStream1, Stream pdfStream2)
    {
        // Ensure the input streams are at the beginning
        pdfStream1.Position = 0;
        pdfStream2.Position = 0;

        // Load the source PDFs directly from the streams using the Document(Stream) constructor
        using (Document doc1 = new Document(pdfStream1))
        using (Document doc2 = new Document(pdfStream2))
        {
            // Create an empty document that will hold the comparison result
            using (Document resultDoc = new Document())
            {
                // Instantiate the graphical comparer (used for visual differences)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Compare page by page – the API expects pages of the same size.
                // We'll compare up to the smaller page count to avoid ArgumentException.
                int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
                for (int i = 1; i <= pageCount; i++)
                {
                    // The overload that writes the diff into an existing Document instance
                    comparer.ComparePagesToPdf(doc1.Pages[i], doc2.Pages[i], resultDoc);
                }

                // Save the result document into a memory stream (no temporary files)
                MemoryStream output = new MemoryStream();
                resultDoc.Save(output);
                // Reset position so the caller can read from the beginning
                output.Position = 0;
                return output; // Caller is responsible for disposing the returned stream
            }
        }
    }

    // Helper to create a simple one‑page PDF containing the supplied text.
    private static MemoryStream CreateSamplePdf(string text)
    {
        MemoryStream ms = new MemoryStream();
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment(text)
            {
                // Simple formatting – optional
                Position = new Position(100, 700)
            };
            page.Paragraphs.Add(tf);
            doc.Save(ms);
        }
        ms.Position = 0; // rewind for reading
        return ms;
    }

    // Example usage
    static void Main()
    {
        // Create two in‑memory PDFs with slight differences
        using (MemoryStream ms1 = CreateSamplePdf("Hello World!"))
        using (MemoryStream ms2 = CreateSamplePdf("Hello Aspose!"))
        {
            using (MemoryStream diffStream = ComparePdfs(ms1, ms2))
            {
                // Write the diff PDF to a file for verification (optional)
                File.WriteAllBytes("diff.pdf", diffStream.ToArray());
            }
        }
    }
}
