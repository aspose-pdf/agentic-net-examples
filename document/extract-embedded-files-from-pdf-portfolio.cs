using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";
        const string outputDir = "ExtractedFiles";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // The EmbeddedFiles collection holds files attached to a PDF portfolio
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                    return;
                }

                // Iterate over each embedded file (FileSpecification) and save it to the output directory
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Use the original file name if available; otherwise generate a unique name
                    string fileName = string.IsNullOrEmpty(fileSpec.Name) ? Guid.NewGuid().ToString() : fileSpec.Name;
                    string destPath = Path.Combine(outputDir, fileName);

                    // Save the embedded file to disk using its Contents stream
                    using (FileStream outStream = File.Create(destPath))
                    using (Stream content = fileSpec.Contents)
                    {
                        content.CopyTo(outStream);
                    }

                    Console.WriteLine($"Extracted: {destPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting embedded files: {ex.Message}");
        }
    }
}
