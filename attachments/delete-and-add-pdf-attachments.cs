using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all existing attachments (embedded files)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing and provides a Delete(string name) method.
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    FileSpecification spec = doc.EmbeddedFiles[i];
                    if (spec != null && !string.IsNullOrEmpty(spec.Name))
                    {
                        doc.EmbeddedFiles.Delete(spec.Name);
                    }
                }
            }

            // Add three new attachments with unique descriptions
            AddAttachment(doc, "file1.txt", "First attachment description");
            AddAttachment(doc, "file2.jpg", "Second attachment description");
            AddAttachment(doc, "file3.pdf", "Third attachment description");

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    static void AddAttachment(Document doc, string filePath, string description)
    {
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {filePath}");
            return;
        }

        // Create a FileSpecification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(filePath), description);
        // Load the file data into a stream and assign it to the specification
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
        // Add the specification to the document's EmbeddedFiles collection
        doc.EmbeddedFiles.Add(fileSpec);
    }
}
