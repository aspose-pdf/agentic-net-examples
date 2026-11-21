using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparer
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string logPath  = "comparison_log.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Open both documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        {
            // Compare page counts.
            int pages1 = doc1.Pages.Count;
            int pages2 = doc2.Pages.Count;

            if (pages1 != pages2)
            {
                logWriter.WriteLine($"PageCountMismatch: {pdfPath1} has {pages1} pages, {pdfPath2} has {pages2} pages.");
            }

            // Determine the number of pages to compare (minimum of both documents).
            int pagesToCompare = Math.Min(pages1, pages2);

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes).
            for (int i = 1; i <= pagesToCompare; i++)
            {
                // Extract text from the current page of each document.
                TextAbsorber absorber1 = new TextAbsorber();
                TextAbsorber absorber2 = new TextAbsorber();

                // Accept the absorber on the specific page.
                doc1.Pages[i].Accept(absorber1);
                doc2.Pages[i].Accept(absorber2);

                string text1 = absorber1.Text ?? string.Empty;
                string text2 = absorber2.Text ?? string.Empty;

                // Simple text comparison – log a mismatch if the texts differ.
                if (!string.Equals(text1, text2, StringComparison.Ordinal))
                {
                    logWriter.WriteLine($"TextMismatch: Page {i}");
                }
            }

            // Flush and close the log file.
            logWriter.Flush();
        }

        Console.WriteLine($"Comparison completed. Log written to '{logPath}'.");
    }
}