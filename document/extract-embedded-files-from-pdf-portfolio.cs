using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";
        const string outputDir = "ExtractedFiles";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Access the collection of embedded files
                var embeddedFiles = doc.EmbeddedFiles;

                // If there are no embedded files, inform the user
                if (embeddedFiles == null || embeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over each embedded file and save it to the output directory
                foreach (var embeddedFile in embeddedFiles)
                {
                    // Use reflection to avoid a direct dependency on the EmbeddedFile type
                    var nameProp = embeddedFile.GetType().GetProperty("Name");
                    var getStreamMethod = embeddedFile.GetType().GetMethod("GetFileStream");

                    string fileName = nameProp?.GetValue(embeddedFile) as string ?? "unknown";
                    string outputPath = Path.Combine(outputDir, fileName);

                    // Retrieve the file stream from the embedded file and write it to disk
                    using (Stream sourceStream = getStreamMethod?.Invoke(embeddedFile, null) as Stream)
                    using (FileStream destStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        sourceStream?.CopyTo(destStream);
                    }

                    Console.WriteLine($"Extracted: {fileName}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting embedded files: {ex.Message}");
        }
    }
}
