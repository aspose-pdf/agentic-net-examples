using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to create two simple PDF files and merge them using Aspose.Pdf.
/// The example follows the mandatory rules: explicit types, deterministic disposal,
/// 1‑based page indexing, and XML documentation for each edit operation.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    public static void Main()
    {
        // ---------------------------------------------------------------------
        // Create first sample PDF (doc1.pdf) with a single blank page.
        // ---------------------------------------------------------------------
        /// <summary>
        /// Creates a simple PDF file named 'doc1.pdf' containing one blank page.
        /// </summary>
        using (Document doc1 = new Document())
        {
            // Add a blank page (page index is 1‑based).
            doc1.Pages.Add();
            // Save the document to the file system.
            doc1.Save("doc1.pdf");
        }

        // ---------------------------------------------------------------------
        // Create second sample PDF (doc2.pdf) with a single blank page.
        // ---------------------------------------------------------------------
        /// <summary>
        /// Creates a simple PDF file named 'doc2.pdf' containing one blank page.
        /// </summary>
        using (Document doc2 = new Document())
        {
            doc2.Pages.Add();
            doc2.Save("doc2.pdf");
        }

        // ---------------------------------------------------------------------
        // Merge the two PDFs into a single document (merged.pdf).
        // ---------------------------------------------------------------------
        /// <summary>
        /// Merges 'doc1.pdf' and 'doc2.pdf' into 'merged.pdf' using PdfFileEditor.
        /// The PdfFileEditor.Concatenate method preserves the original page order.
        /// </summary>
        string[] sourceFiles = new string[] { "doc1.pdf", "doc2.pdf" };
        string outputFile = "merged.pdf";

        // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient.
        PdfFileEditor editor = new PdfFileEditor();
        // Concatenate the source PDFs into the output PDF.
        editor.Concatenate(sourceFiles, outputFile);

        // Optionally, inform the user that the operation completed.
        Console.WriteLine("PDF files merged successfully into 'merged.pdf'.");
    }
}
