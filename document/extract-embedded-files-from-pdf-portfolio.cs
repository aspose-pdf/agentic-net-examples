using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        // Input PDF portfolio containing embedded files
        const string inputPdfPath = "portfolio.pdf";

        // Directory where extracted files will be saved
        const string outputDirectory = "ExtractedFiles";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Access the collection of embedded files
                EmbeddedFileCollection embeddedFiles = pdfDoc.EmbeddedFiles;

                // If there are no embedded files, inform the user
                if (embeddedFiles == null || embeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over each embedded file and save it to the output directory using reflection
                foreach (var embeddedFile in embeddedFiles)
                {
                    // Retrieve the file name via reflection
                    var nameProp = embeddedFile.GetType().GetProperty("Name");
                    string fileName = nameProp?.GetValue(embeddedFile) as string ?? "unknown.bin";

                    // Build the full path for the extracted file
                    string outputPath = Path.Combine(outputDirectory, fileName);

                    // Invoke the Save(string) method via reflection
                    var saveMethod = embeddedFile.GetType().GetMethod("Save", new[] { typeof(string) });
                    if (saveMethod != null)
                    {
                        saveMethod.Invoke(embeddedFile, new object[] { outputPath });
                        Console.WriteLine($"Extracted: {fileName} → {outputPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Unable to save embedded file: {fileName}");
                    }
                }
            }

            Console.WriteLine("Extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
