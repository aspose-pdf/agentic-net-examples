using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string sourceFolder = "SourcePdfs";
        // Folder where sanitized PDFs will be written
        const string targetFolder = "SanitizedPdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure the target folder exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (append "_clean" to avoid overwriting)
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(targetFolder, fileName + "_clean.pdf");

            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // ==== Sanitization steps ====

                    // 1. Clear document metadata (title, author, etc.)
                    doc.Info.Title        = "";
                    doc.Info.Author       = "";
                    doc.Info.Subject      = "";
                    doc.Info.Keywords     = "";
                    doc.Info.Creator      = "";
                    doc.Info.Producer     = "";
                    doc.Info.ModDate      = DateTime.Now;
                    doc.Info.CreationDate = DateTime.Now;

                    // 2. Remove any embedded JavaScript actions
                    // The JavaScriptCollection does not expose Count/Clear. Remove each entry via its key.
                    if (doc.JavaScript != null && doc.JavaScript.Keys != null && doc.JavaScript.Keys.Count > 0)
                    {
                        // Collect keys first to avoid modifying the collection while iterating
                        var keys = new System.Collections.Generic.List<string>(doc.JavaScript.Keys);
                        foreach (string key in keys)
                        {
                            doc.JavaScript.Remove(key);
                        }
                    }
                    // Also clear the document level OpenAction if it contains JavaScript
                    doc.OpenAction = null;

                    // 3. Remove embedded files (attachments)
                    if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                    {
                        // Delete each embedded file by its name (1‑based indexing)
                        for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                        {
                            var fileSpec = doc.EmbeddedFiles[i];
                            if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                            {
                                doc.EmbeddedFiles.Delete(fileSpec.Name);
                            }
                        }
                    }

                    // 4. Remove all annotations from every page
                    foreach (Page page in doc.Pages)
                    {
                        if (page.Annotations != null && page.Annotations.Count > 0)
                        {
                            // Annotations collection also uses 1‑based indexing; clear by deleting each.
                            for (int i = page.Annotations.Count; i >= 1; i--)
                            {
                                page.Annotations.Delete(i);
                            }
                        }
                    }

                    // Save the sanitized PDF (PDF format is default)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}
