using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetFile = "removeMe.txt"; // filename of the attachment to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Embedded files are stored in the Document.EmbeddedFiles collection.
            // Collect the names of attachments that match the target filename.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                var namesToDelete = new List<string>();
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (string.Equals(fileSpec.Name, targetFile, StringComparison.OrdinalIgnoreCase))
                    {
                        namesToDelete.Add(fileSpec.Name);
                    }
                }

                // Delete the collected attachments.
                foreach (string name in namesToDelete)
                {
                    doc.EmbeddedFiles.Delete(name);
                    Console.WriteLine($"Removed attachment: {name}");
                }
            }

            // Save the modified PDF; the target path can be the same or different file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without '{targetFile}' to '{outputPath}'.");
    }
}
