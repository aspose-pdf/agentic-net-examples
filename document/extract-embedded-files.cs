using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputDir = "ExtractedFiles";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // In Aspose.Pdf the embedded files are represented by FileSpecification objects.
            foreach (FileSpecification embedded in doc.EmbeddedFiles)
            {
                // Determine a safe file name.
                string fileName = string.IsNullOrEmpty(embedded.Name) ? Guid.NewGuid().ToString() : embedded.Name;
                string filePath = Path.Combine(outputDir, fileName);

                // The FileSpecification does not expose a Save method. Use its Contents stream to write the file.
                using (FileStream outStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    if (embedded.Contents != null)
                    {
                        embedded.Contents.CopyTo(outStream);
                    }
                }

                Console.WriteLine($"Saved embedded file: {fileName}");
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
