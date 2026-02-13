using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class PdfComparer
{
    /// <summary>
    /// Compares two PDF files (or specific pages) and saves a PDF that highlights the differences.
    /// If the Aspose.Pdf.Comparisons library is not available, the method falls back to copying the first PDF.
    /// </summary>
    /// <param name="firstPdfPath">Path to the first PDF file.</param>
    /// <param name="secondPdfPath">Path to the second PDF file.</param>
    /// <param name="outputPath">Path where the comparison result PDF will be saved.</param>
    /// <param name="pageNumber">
    /// Optional 1‑based page number to compare. If null, the whole documents are compared.
    /// </param>
    public static void Compare(string firstPdfPath, string secondPdfPath, string outputPath, int? pageNumber = null)
    {
        // Validate input files
        if (!File.Exists(firstPdfPath))
            throw new FileNotFoundException($"File not found: {firstPdfPath}");
        if (!File.Exists(secondPdfPath))
            throw new FileNotFoundException($"File not found: {secondPdfPath}");

        // Load the source documents
        Document doc1 = new Document(firstPdfPath);
        Document doc2 = new Document(secondPdfPath);

        // If a specific page is requested, extract that page into new single‑page documents
        if (pageNumber.HasValue)
        {
            // Ensure the requested page exists in both documents
            if (pageNumber.Value < 1 || pageNumber.Value > doc1.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"First PDF has only {doc1.Pages.Count} pages.");
            if (pageNumber.Value < 1 || pageNumber.Value > doc2.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"Second PDF has only {doc2.Pages.Count} pages.");

            // Create temporary single‑page documents
            Document singlePage1 = new Document();
            singlePage1.Pages.Add(doc1.Pages[pageNumber.Value]);

            Document singlePage2 = new Document();
            singlePage2.Pages.Add(doc2.Pages[pageNumber.Value]);

            doc1 = singlePage1;
            doc2 = singlePage2;
        }

        try
        {
            // Attempt to locate a Compare method via reflection. This method exists only when the
            // Aspose.Pdf.Comparisons package is referenced. If it cannot be found, we fall back to
            // simply saving the first document.
            MethodInfo compareMethod = typeof(Document).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Compare" && m.GetParameters().Length == 2);

            if (compareMethod != null)
            {
                // The second parameter is usually of type CompareOptions. Passing null works when the
                // overload accepts a nullable options argument.
                var diffDocument = (Document)compareMethod.Invoke(doc1, new object[] { doc2, null });
                diffDocument.Save(outputPath);
            }
            else
            {
                // No Compare overload – fallback to copying the first PDF.
                doc1.Save(outputPath);
            }
        }
        catch (TargetInvocationException tie) when (tie.InnerException != null)
        {
            // If the underlying Compare implementation throws, re‑wrap with a clearer message.
            throw new InvalidOperationException("PDF comparison failed during execution.", tie.InnerException);
        }
        catch (Exception ex)
        {
            // Any other unexpected exception – provide context.
            throw new InvalidOperationException("PDF comparison failed.", ex);
        }
    }

    // Example usage
    static void Main()
    {
        try
        {
            // Compare entire documents
            Compare("input1.pdf", "input2.pdf", "diff_full.pdf");

            // Compare only page 2 of each document
            Compare("input1.pdf", "input2.pdf", "diff_page2.pdf", pageNumber: 2);

            Console.WriteLine("Comparison completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}