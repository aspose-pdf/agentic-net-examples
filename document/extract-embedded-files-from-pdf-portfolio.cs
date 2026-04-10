using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF portfolio containing embedded files
        const string inputPdfPath = "portfolio.pdf";

        // Directory where extracted files will be saved
        const string outputDirectory = "ExtractedFiles";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Access the collection of embedded files
            EmbeddedFileCollection embeddedFiles = pdfDocument.EmbeddedFiles;

            // Check if there are any embedded files
            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Iterate over each embedded file and save it to the output directory
            foreach (var embeddedFile in embeddedFiles)
            {
                // Use reflection to obtain the file name (property "Name")
                var nameProp = embeddedFile.GetType().GetProperty("Name");
                string fileName = nameProp?.GetValue(embeddedFile) as string ?? "unknown";

                // Build the full path for the extracted file
                string outputFilePath = Path.Combine(outputDirectory, fileName);

                // Try to invoke the Save(string) method via reflection
                var saveMethod = embeddedFile.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(embeddedFile, new object[] { outputFilePath });
                }
                else
                {
                    // Fallback: retrieve the underlying file specification stream and copy it manually
                    var fileSpec = embeddedFile.GetType().GetProperty("FileSpecification")?.GetValue(embeddedFile);
                    var contents = fileSpec?.GetType().GetProperty("Contents")?.GetValue(fileSpec) as Stream;
                    if (contents != null)
                    {
                        using (var outStream = File.Create(outputFilePath))
                        {
                            contents.CopyTo(outStream);
                        }
                    }
                }

                Console.WriteLine($"Extracted: {outputFilePath}");
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
