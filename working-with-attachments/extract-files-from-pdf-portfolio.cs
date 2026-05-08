using System;
using System.IO;
using Aspose.Pdf;

class PortfolioExtractor
{
    static void Main()
    {
        // Input PDF portfolio file
        const string inputPdf = "portfolio.pdf";

        // Output root directory where extracted files will be placed
        const string outputRoot = "ExtractedFiles";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output root directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (portfolio) inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // The EmbeddedFiles collection holds all files stored in the portfolio.
            // Use reflection to avoid a direct compile‑time dependency on the EmbeddedFile type.
            foreach (var embedded in doc.EmbeddedFiles)
            {
                // Retrieve the file name (may contain relative path information)
                var nameProp = embedded.GetType().GetProperty("Name");
                string relativePath = nameProp?.GetValue(embedded) as string ?? string.Empty;
                if (string.IsNullOrEmpty(relativePath))
                {
                    // Skip entries without a valid name
                    continue;
                }

                // Convert any forward slashes to the platform's directory separator
                relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar);

                // Combine with the output root to get the full destination path
                string destinationPath = Path.Combine(outputRoot, relativePath);

                // Ensure the target directory exists before saving the file
                string destinationDir = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                // Invoke the Save(string) method via reflection
                var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(embedded, new object[] { destinationPath });
                    Console.WriteLine($"Extracted: {destinationPath}");
                }
                else
                {
                    // Fallback: try to read the file specification stream directly
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
                        Console.WriteLine($"Extracted (stream fallback): {destinationPath}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unable to extract embedded file: {relativePath}");
                    }
                }
            }
        }

        Console.WriteLine("Portfolio extraction completed.");
    }
}
