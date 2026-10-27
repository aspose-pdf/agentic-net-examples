using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "remove_me.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                bool removed = false;

                // Iterate over the embedded files collection to find the target by filename
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (string.Equals(fileSpec.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Remove the attachment while keeping others intact
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                        removed = true;
                        break;
                    }
                }

                if (!removed)
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved without '{attachmentName}' to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
