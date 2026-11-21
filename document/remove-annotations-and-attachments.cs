using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ---------- Remove all annotations (comments, markup, etc.) ----------
                // Annotations are stored per page. Iterate pages and delete each annotation.
                foreach (Page page in doc.Pages)
                {
                    if (page.Annotations != null && page.Annotations.Count > 0)
                    {
                        // AnnotationCollection uses 1‑based indexing. Delete from the end to avoid re‑indexing issues.
                        for (int i = page.Annotations.Count; i >= 1; i--)
                        {
                            page.Annotations.Delete(i);
                        }
                    }
                }

                // ---------- Remove all embedded files (attachments) ----------
                // EmbeddedFileCollection also uses 1‑based indexing and provides a Delete(string name) overload.
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
