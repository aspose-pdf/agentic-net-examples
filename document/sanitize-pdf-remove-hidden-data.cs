using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the PDF to be sanitized
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Sanitizer <input-pdf-path>");
            return;
        }

        string inputPath = args[0];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Build the output path by appending "_sanitized" before the extension
        string directory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Perform manual sanitization (Aspose.Pdf.Document does not expose a Sanitize() method)
                SanitizeDocument(doc);

                // Save the sanitized document as a PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes hidden data, metadata, JavaScript, embedded files and other potentially sensitive information.
    /// </summary>
    /// <param name="doc">The Aspose.Pdf.Document to sanitize.</param>
    private static void SanitizeDocument(Document doc)
    {
        // 1. Clear document metadata (Info and XMP metadata)
        if (doc.Info != null)
        {
            doc.Info.Title = null;
            doc.Info.Author = null;
            doc.Info.Subject = null;
            doc.Info.Keywords = null;
            doc.Info.Creator = null;
            doc.Info.Producer = null;
            // CreationDate and ModDate are non‑nullable in some versions; omit setting them.
        }
        // Clear XMP metadata if any entries exist
        if (doc.Metadata != null && doc.Metadata.Count > 0)
        {
            doc.Metadata.Clear();
        }

        // 2. Remove JavaScript (document level and page level)
        doc.OpenAction = null; // document open action
        // JavaScriptCollection does not expose Count; use Keys collection instead
        if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
        {
            // Copy keys to a list to avoid modifying the collection while iterating
            var keys = new List<string>(doc.JavaScript.Keys);
            foreach (var key in keys)
            {
                doc.JavaScript.Remove(key);
            }
        }
        // Page‑level JavaScript actions
        foreach (Page page in doc.Pages)
        {
            if (page.Actions != null)
            {
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }
        }

        // 3. Remove embedded files (FileAttachmentAnnotation) from all pages
        foreach (Page page in doc.Pages)
        {
            // Iterate backwards because we will delete items
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                if (page.Annotations[i] is FileAttachmentAnnotation)
                {
                    page.Annotations.Delete(i);
                }
            }
        }

        // 4. Additional hidden content removal can be added here (e.g., invisible annotations).
    }
}
