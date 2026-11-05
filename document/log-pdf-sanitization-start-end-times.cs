using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";
        const string logPath    = "sanitization_audit.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open log file for appending audit entries
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            // Record overall operation start
            DateTime overallStart = DateTime.Now;
            logWriter.WriteLine($"Sanitization started: {overallStart:O}");

            // Load PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // ---------- Remove document metadata ----------
                DateTime metaStart = DateTime.Now;
                logWriter.WriteLine($"Metadata removal started: {metaStart:O}");

                // Clear common metadata fields
                doc.Info.Title = null;
                doc.Info.Author = null;
                doc.Info.Subject = null;
                doc.Info.Keywords = null;
                doc.Info.Creator = null;
                doc.Info.Producer = null;
                // Date fields are DateTime structs; use MinValue to clear them
                doc.Info.CreationDate = DateTime.MinValue;
                doc.Info.ModDate = DateTime.MinValue;

                DateTime metaEnd = DateTime.Now;
                logWriter.WriteLine($"Metadata removal completed: {metaEnd:O} (Duration: {(metaEnd - metaStart).TotalMilliseconds} ms)");

                // ---------- Remove all annotations ----------
                DateTime annotStart = DateTime.Now;
                logWriter.WriteLine($"Annotation removal started: {annotStart:O}");

                // Pages are 1‑based (page-indexing-one-based rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    // Clear the Annotations collection
                    page.Annotations.Clear();
                }

                DateTime annotEnd = DateTime.Now;
                logWriter.WriteLine($"Annotation removal completed: {annotEnd:O} (Duration: {(annotEnd - annotStart).TotalMilliseconds} ms)");

                // ---------- Save sanitized PDF ----------
                DateTime saveStart = DateTime.Now;
                logWriter.WriteLine($"Save operation started: {saveStart:O}");

                // Save to PDF (no special SaveOptions needed)
                doc.Save(outputPath);

                DateTime saveEnd = DateTime.Now;
                logWriter.WriteLine($"Save operation completed: {saveEnd:O} (Duration: {(saveEnd - saveStart).TotalMilliseconds} ms)");
            }

            // Record overall operation end
            DateTime overallEnd = DateTime.Now;
            logWriter.WriteLine($"Sanitization finished: {overallEnd:O} (Total duration: {(overallEnd - overallStart).TotalMilliseconds} ms)");
            logWriter.WriteLine(new string('-', 80));
        }

        Console.WriteLine($"Sanitization completed. Output saved to '{outputPath}'. Audit log written to '{logPath}'.");
    }
}
