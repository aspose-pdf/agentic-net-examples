using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";
        const string outputDir = "ExtractedFiles";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Iterate over embedded files using reflection to avoid direct dependency on the EmbeddedFile type
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // Get the file name
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string fileName = nameProp?.GetValue(embedded) as string;
                    if (string.IsNullOrEmpty(fileName))
                        continue;

                    // Build the output path
                    string filePath = Path.Combine(outputDir, fileName);

                    // Invoke the Save(string) method via reflection
                    var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embedded, new object[] { filePath });
                        Console.WriteLine($"Saved embedded file: {filePath}");
                    }
                    else
                    {
                        // Fallback: try to extract the raw stream if Save method is unavailable
                        var fileSpecProp = embedded.GetType().GetProperty("FileSpecification");
                        var fileSpec = fileSpecProp?.GetValue(embedded);
                        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                        var contents = contentsProp?.GetValue(fileSpec) as Stream;
                        if (contents != null)
                        {
                            using (var outStream = File.Create(filePath))
                                contents.CopyTo(outStream);
                            Console.WriteLine($"Saved embedded file (stream fallback): {filePath}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting embedded files: {ex.Message}");
        }
    }
}
