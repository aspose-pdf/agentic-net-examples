using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            EmbeddedFileCollection embedded = doc.EmbeddedFiles;
            List<string> toDelete = new List<string>();

            // Collect attachment names that match the pattern using 1‑based indexing
            for (int i = 1; i <= embedded.Count; i++)
            {
                FileSpecification fileSpec = embedded[i];
                string name = fileSpec.Name;
                if (!string.IsNullOrEmpty(name) && name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    toDelete.Add(name);
                }
            }

            // Delete the collected attachments
            foreach (string name in toDelete)
            {
                embedded.Delete(name);
                Console.WriteLine($"Deleted attachment: {name}");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
    }
}
