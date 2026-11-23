using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Attachments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all embedded files (FileSpecification objects) in the PDF
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Use only the file name part to avoid path‑traversal issues
                    string fileName = Path.GetFileName(fileSpec.Name);
                    string outPath = Path.Combine(outputFolder, fileName);

                    // Save the embedded file by copying its Contents stream to a file on disk
                    using (FileStream outStream = File.Create(outPath))
                    using (Stream content = fileSpec.Contents)
                    {
                        content.CopyTo(outStream);
                    }

                    Console.WriteLine($"Saved attachment: {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
