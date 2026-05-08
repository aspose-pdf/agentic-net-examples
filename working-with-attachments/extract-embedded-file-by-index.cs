using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";   // input PDF containing embedded files
        const int index = 1;                     // 1‑based index of the desired embedded file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the requested index exists (EmbeddedFileCollection is 1‑based)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count < index)
            {
                Console.Error.WriteLine($"No embedded file at index {index}.");
                return;
            }

            // Retrieve the embedded file specification
            FileSpecification fileSpec = doc.EmbeddedFiles[index];

            // Preserve the original file name (includes its extension)
            string outputFileName = !string.IsNullOrEmpty(fileSpec.Name)
                ? fileSpec.Name
                : $"embedded_{index}";

            // Build the full output path (current directory)
            string outputPath = Path.Combine(Environment.CurrentDirectory, outputFileName);

            // Extract the embedded file using the Contents stream (EmbeddedFile property does not exist)
            if (fileSpec.Contents != null)
            {
                using (FileStream outStream = File.Create(outputPath))
                {
                    fileSpec.Contents.CopyTo(outStream);
                }
                Console.WriteLine($"Embedded file saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Embedded file data is missing.");
            }
        }
    }
}
