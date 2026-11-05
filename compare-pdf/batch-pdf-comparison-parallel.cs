using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfBatchComparer
{
    static void Main()
    {
        const string referencePath = "reference.pdf";
        const string folderPath = "pdfs";
        const int maxDegree = 4; // limit concurrency to avoid high memory usage

        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePath}");
            return;
        }

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Collect all PDF files in the folder, excluding the reference file itself
        List<string> pdfFiles = new List<string>();
        foreach (string file in Directory.EnumerateFiles(folderPath, "*.pdf"))
        {
            if (!string.Equals(Path.GetFullPath(file), Path.GetFullPath(referencePath), StringComparison.OrdinalIgnoreCase))
                pdfFiles.Add(file);
        }

        // Thread‑safe collection for mismatches or errors
        ConcurrentBag<string> mismatched = new ConcurrentBag<string>();

        // Parallel processing with a bounded degree of parallelism
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = maxDegree };

        Parallel.ForEach(pdfFiles, options, pdfPath =>
        {
            try
            {
                // Load both reference and target PDFs inside using blocks for deterministic disposal
                using (Document refDoc = new Document(referencePath))
                using (Document targetDoc = new Document(pdfPath))
                {
                    // Quick check: page counts must be equal
                    if (refDoc.Pages.Count != targetDoc.Pages.Count)
                    {
                        mismatched.Add($"{pdfPath} – page count differs (ref:{refDoc.Pages.Count}, target:{targetDoc.Pages.Count})");
                        return;
                    }

                    // Extract full text from each document
                    string refText = ExtractText(refDoc);
                    string targetText = ExtractText(targetDoc);

                    // Compare extracted text
                    if (!string.Equals(refText, targetText, StringComparison.Ordinal))
                    {
                        mismatched.Add($"{pdfPath} – text content differs");
                    }
                }
            }
            catch (Exception ex)
            {
                mismatched.Add($"{pdfPath} – error: {ex.Message}");
            }
        });

        // Output summary
        Console.WriteLine("Comparison completed.");
        if (mismatched.IsEmpty)
        {
            Console.WriteLine("All PDFs match the reference.");
        }
        else
        {
            Console.WriteLine("Mismatches / errors:");
            foreach (string msg in mismatched)
            {
                Console.WriteLine(msg);
            }
        }
    }

    // Helper to extract all text from a Document using TextAbsorber
    private static string ExtractText(Document doc)
    {
        TextAbsorber absorber = new TextAbsorber();
        doc.Pages.Accept(absorber);
        return absorber.Text;
    }
}