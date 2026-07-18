using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF Portfolio file
        const string inputPdf = "portfolio.pdf";

        // Root directory where extracted files will be written
        const string outputRoot = "ExtractedFiles";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF Portfolio inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Iterate over all embedded files in the portfolio using reflection
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // Retrieve the file name via reflection (property "Name")
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string relativePath = nameProp?.GetValue(embedded) as string ?? "UnnamedFile";

                    // Determine the full path on disk where the file will be saved
                    string fullPath = Path.Combine(outputRoot, relativePath);

                    // Ensure the directory hierarchy exists
                    string? directory = Path.GetDirectoryName(fullPath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Invoke the Save(string) method via reflection
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { fullPath });
                        Console.WriteLine($"Extracted: {fullPath}");
                    }
                    else
                    {
                        // Fallback: try to copy the raw stream if Save method is unavailable
                        var fileSpecProp = embedded.GetType().GetProperty("FileSpecification");
                        var fileSpec = fileSpecProp?.GetValue(embedded);
                        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                        var contents = contentsProp?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var outStream = File.Create(fullPath))
                            {
                                contents.CopyTo(outStream);
                            }
                            Console.WriteLine($"Extracted (stream): {fullPath}");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unable to extract embedded file: {relativePath}");
                        }
                    }
                }
            }

            Console.WriteLine("All embedded files have been processed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
