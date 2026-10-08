using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // EmbeddedFiles collection uses 1‑based indexing
                int fileCount = doc.EmbeddedFiles.Count;

                for (int i = 1; i <= fileCount; i++)
                {
                    // Retrieve the file specification for each embedded file
                    FileSpecification fileSpec = doc.EmbeddedFiles[i];
                    string fileName = fileSpec.Name ?? $"attachment_{i}";

                    // Build a unique output path (handle possible name collisions)
                    string outPath = Path.Combine(outputDir, fileName);
                    int duplicateIndex = 1;
                    while (File.Exists(outPath))
                    {
                        string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                        string ext = Path.GetExtension(fileName);
                        outPath = Path.Combine(outputDir, $"{nameOnly}_{duplicateIndex}{ext}");
                        duplicateIndex++;
                    }

                    // Copy the embedded file's content stream to the output file
                    using (FileStream outStream = File.Create(outPath))
                    using (Stream content = fileSpec.Contents)
                    {
                        content.CopyTo(outStream);
                    }

                    Console.WriteLine($"Extracted: {outPath}");
                }

                if (fileCount == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}