using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the input PDF.
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: Sanitizer.exe <input-pdf-path>");
            return;
        }

        string inputPath = args[0];

        // Verify that the file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Build the output path by inserting "_sanitized" before the extension.
        string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // -----------------------------------------------------------------
                // Hidden‑data sanitization.
                // -----------------------------------------------------------------
                // Configure sanitization options. The current Aspose.Pdf API provides
                // RemoveAnnotations, RemoveMetadata and RemoveJavaScriptsAndActions.
                // Embedded files are removed manually because the options class does
                // not expose a dedicated flag.
                // -----------------------------------------------------------------
                var options = new HiddenDataSanitizationOptions
                {
                    RemoveAnnotations = true,
                    RemoveMetadata = true,
                    RemoveJavaScriptsAndActions = true
                };

                var sanitizer = new HiddenDataSanitizer(options);
                sanitizer.Sanitize(doc);

                // Manually delete any embedded files (if present).
                if (doc.EmbeddedFiles != null)
                {
                    doc.EmbeddedFiles.Delete();
                }

                // Save the sanitized PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
