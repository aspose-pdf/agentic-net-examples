using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";
        const string outputDir = "ExtractedFiles";

        // Verify the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF portfolio.
            using (Document doc = new Document(inputPdf))
            {
                // The EmbeddedFiles collection holds all files attached to the portfolio.
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // EmbeddedFileCollection uses 1‑based indexing and does not provide an ExtractAll method.
                    // Iterate through each embedded file, copy its content stream to a file on disk,
                    // and preserve any folder hierarchy encoded in the file name.
                    for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                    {
                        FileSpecification fileSpec = doc.EmbeddedFiles[i];
                        string attachmentName = fileSpec.Name ?? $"attachment_{i}";

                        // Build the full path where the file will be written.
                        string targetPath = Path.Combine(outputDir, attachmentName);
                        string targetDir = Path.GetDirectoryName(targetPath);
                        if (!string.IsNullOrEmpty(targetDir))
                        {
                            Directory.CreateDirectory(targetDir);
                        }

                        // Copy the embedded file's content stream to the target file.
                        using (Stream source = fileSpec.Contents)
                        using (FileStream destination = File.Create(targetPath))
                        {
                            source.CopyTo(destination);
                        }
                    }

                    Console.WriteLine($"Extracted {doc.EmbeddedFiles.Count} file(s) to '{outputDir}'.");
                }
                else
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
