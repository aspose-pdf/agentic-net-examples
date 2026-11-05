using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the input PDF
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: Sanitizer <input-pdf-path>");
            return;
        }

        string inputPath = args[0];

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Build the output path by inserting "_sanitized" before the extension
        string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // -----------------------------------------------------------------
                // Hidden‑data sanitisation fallback.
                // The Document.Sanitize() method is only available in newer Aspose.Pdf
                // releases. When compiling against an older version the method does not
                // exist (CS1061). To achieve a similar effect we manually remove data that
                // is commonly considered "hidden":
                //   • Annotations (including comments, stamps, etc.)
                //   • Document metadata (title, author, subject, keywords, etc.)
                //   • Embedded files (attachments, portfolios)
                //   • XMP metadata (if present)
                // -----------------------------------------------------------------

                // 1. Remove all annotations from every page
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }

                // 2. Clear standard document information fields
                DocumentInfo info = doc.Info;
                info.Title = string.Empty;
                info.Author = string.Empty;
                info.Subject = string.Empty;
                info.Keywords = string.Empty;
                info.Creator = string.Empty;
                info.Producer = string.Empty;
                info.CreationDate = DateTime.MinValue;
                // Use the correct property name for modification date
                info.ModDate = DateTime.MinValue;

                // 3. Remove any embedded files (attachments, portfolio items)
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // EmbeddedFileCollection uses 1‑based indexing; delete by name.
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // 4. Remove XMP metadata if it exists
                if (doc.Metadata != null)
                {
                    // The Metadata property is read‑only; clear its contents instead of assigning null.
                    doc.Metadata.Clear();
                }

                // If you are using a newer Aspose.Pdf version that provides Document.Sanitize(),
                // you can uncomment the following line instead of the manual steps above:
                // doc.Sanitize();

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
