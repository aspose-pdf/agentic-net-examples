using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string resultPdf = "diff_output.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs to be compared
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options.
            // Setting ExcludeTables to false ensures that all content,
            // including form field values, participates in the diff.
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeTables = false
            };

            // Perform page‑by‑page comparison and save the diff PDF.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdf);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdf}'.");
    }
}