using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfComparer
{
    /// <summary>
    /// Compares two PDF files by placing their pages sequentially in a single output PDF.
    /// The method loads the source PDFs from memory streams, copies their pages into a new
    /// document, and returns the resulting PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes1">First PDF content.</param>
    /// <param name="pdfBytes2">Second PDF content.</param>
    /// <returns>Byte array containing the comparison PDF.</returns>
    public static byte[] ComparePdfs(byte[] pdfBytes1, byte[] pdfBytes2)
    {
        // Load the first PDF from a memory stream
        using (MemoryStream ms1 = new MemoryStream(pdfBytes1))
        using (Document doc1 = new Document(ms1))
        // Load the second PDF from a memory stream
        using (MemoryStream ms2 = new MemoryStream(pdfBytes2))
        using (Document doc2 = new Document(ms2))
        // Create the output document that will hold the comparison result
        using (Document diffDoc = new Document())
        {
            // -----------------------------------------------------------------
            // Add a title page for the comparison result
            // -----------------------------------------------------------------
            Page titlePage = diffDoc.Pages.Add();
            TextFragment title = new TextFragment("PDF Comparison Result");
            title.Position = new Position(0, 800);
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.FontSize = 24;
            title.TextState.ForegroundColor = Color.FromRgb(0, 0, 0.8);
            titlePage.Paragraphs.Add(title);

            // -----------------------------------------------------------------
            // Copy pages from the first document
            // -----------------------------------------------------------------
            for (int i = 1; i <= doc1.Pages.Count; i++) // 1‑based indexing
            {
                // New API: add the source page to the target document
                diffDoc.Pages.Add(doc1.Pages[i]);
            }

            // -----------------------------------------------------------------
            // Insert a separator page to distinguish the two source PDFs
            // -----------------------------------------------------------------
            Page separator = diffDoc.Pages.Add();
            TextFragment sepText = new TextFragment("----- End of Document 1 -----");
            sepText.Position = new Position(0, 800);
            sepText.TextState.Font = FontRepository.FindFont("Helvetica");
            sepText.TextState.FontSize = 16;
            sepText.TextState.ForegroundColor = Color.FromRgb(0.5, 0, 0);
            separator.Paragraphs.Add(sepText);

            // -----------------------------------------------------------------
            // Copy pages from the second document
            // -----------------------------------------------------------------
            for (int i = 1; i <= doc2.Pages.Count; i++) // 1‑based indexing
            {
                diffDoc.Pages.Add(doc2.Pages[i]);
            }

            // -----------------------------------------------------------------
            // Save the resulting document to a memory stream and return the bytes
            // -----------------------------------------------------------------
            using (MemoryStream outputMs = new MemoryStream())
            {
                diffDoc.Save(outputMs);
                return outputMs.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main()
    {
        // No operation – this method exists only to provide a valid entry point.
    }
}