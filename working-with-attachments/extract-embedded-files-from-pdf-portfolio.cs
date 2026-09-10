using System;
using System.IO;
using Aspose.Pdf;

class PortfolioExtractor
{
    static void Main()
    {
        // Input PDF portfolio file
        const string inputPdf = "portfolio.pdf";
        // Output directory where extracted files will be saved
        const string outputDir = "ExtractedFiles";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the output root directory exists
                Directory.CreateDirectory(outputDir);

                // Check if the document contains any embedded files (portfolio items)
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over each embedded file in the portfolio using reflection to avoid
                // a direct compile‑time dependency on the EmbeddedFile type (which may not be
                // available in the core Aspose.Pdf namespace).
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // Retrieve the virtual path/name of the embedded file.
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string virtualPath = nameProp?.GetValue(embedded) as string ?? "UnnamedFile";

                    // Build the full destination path on the local file system.
                    string destinationPath = Path.Combine(outputDir, virtualPath);

                    // Ensure the directory hierarchy exists before saving the file.
                    string destinationDir = Path.GetDirectoryName(destinationPath);
                    if (!string.IsNullOrEmpty(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }

                    // Try to invoke the Save(string) method that Aspose.Pdf provides for embedded files.
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { destinationPath });
                    }
                    else
                    {
                        // Fallback: extract the raw stream from the FileSpecification if Save is unavailable.
                        var fileSpecProp = embedded.GetType().GetProperty("FileSpecification");
                        var fileSpec = fileSpecProp?.GetValue(embedded);
                        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                        var contents = contentsProp?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var outStream = File.Create(destinationPath))
                            {
                                contents.CopyTo(outStream);
                            }
                        }
                    }

                    Console.WriteLine($"Extracted: {virtualPath} -> {destinationPath}");
                }
            }

            Console.WriteLine("All embedded files have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
