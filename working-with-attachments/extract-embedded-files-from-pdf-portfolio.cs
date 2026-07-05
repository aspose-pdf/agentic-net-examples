using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class PortfolioExtractor
{
    static void Main()
    {
        // Input PDF portfolio file
        const string inputPdfPath = "portfolio.pdf";

        // Output base directory where extracted files will be placed
        const string outputBaseDir = "ExtractedFiles";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output base directory exists
        Directory.CreateDirectory(outputBaseDir);

        try
        {
            // Load the PDF document (portfolio) using a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // The EmbeddedFiles collection holds all files embedded in the portfolio
                if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over each embedded file using reflection to avoid direct dependency on the EmbeddedFile type
                foreach (var embedded in pdfDoc.EmbeddedFiles)
                {
                    // Retrieve the file name (may contain sub‑folder structure)
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string relativePath = nameProp?.GetValue(embedded) as string ?? "UnnamedFile";

                    // Determine any sub‑folder hierarchy encoded in the name
                    string subFolder = Path.GetDirectoryName(relativePath) ?? string.Empty;

                    // Build the full directory path where this file will be saved
                    string targetDir = Path.Combine(outputBaseDir, subFolder);
                    Directory.CreateDirectory(targetDir); // Create subfolders as needed

                    // Build the full file path (including file name)
                    string targetFilePath = Path.Combine(targetDir, Path.GetFileName(relativePath));

                    // Invoke the Save(string) method via reflection
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { targetFilePath });
                        Console.WriteLine($"Extracted: {targetFilePath}");
                    }
                    else
                    {
                        // Fallback: try to obtain the raw stream from the FileSpecification if Save is unavailable
                        var fileSpecProp = embedded.GetType().GetProperty("FileSpecification");
                        var fileSpec = fileSpecProp?.GetValue(embedded);
                        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                        var contents = contentsProp?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var outStream = File.Create(targetFilePath))
                            {
                                contents.CopyTo(outStream);
                            }
                            Console.WriteLine($"Extracted (stream fallback): {targetFilePath}");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unable to extract embedded file: {relativePath}");
                        }
                    }
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
