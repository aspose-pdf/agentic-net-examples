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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            // Collect the names of attachments that match the pattern
            List<string> namesToDelete = new List<string>();
            foreach (var fileObj in attachments)
            {
                // Use reflection to obtain the Name property (avoids direct dependency on EmbeddedFile type)
                var nameProp = fileObj?.GetType().GetProperty("Name");
                string name = nameProp?.GetValue(fileObj) as string;
                if (!string.IsNullOrEmpty(name) &&
                    name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    namesToDelete.Add(name);
                }
            }

            // Delete the collected attachments
            foreach (string name in namesToDelete)
            {
                attachments.Delete(name);
                Console.WriteLine($"Deleted attachment: {name}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
