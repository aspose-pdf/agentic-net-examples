using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and attachment name
        const string pdfPath = "input.pdf";
        const string attachmentName = "myfile.txt";
        const string outputDirectory = "Attachments";

        // Verify PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Locate the attachment by name (case‑insensitive) using FileSpecification
                FileSpecification targetSpec = null;
                foreach (FileSpecification spec in doc.EmbeddedFiles)
                {
                    if (string.Equals(spec.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetSpec = spec;
                        break;
                    }
                }

                if (targetSpec == null)
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                    return;
                }

                // Build full path for the extracted file
                string outputPath = Path.Combine(outputDirectory, targetSpec.Name);

                // Write the attachment's binary content to disk using the Contents stream
                using (FileStream outStream = File.Create(outputPath))
                using (Stream content = targetSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                Console.WriteLine($"Attachment saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
