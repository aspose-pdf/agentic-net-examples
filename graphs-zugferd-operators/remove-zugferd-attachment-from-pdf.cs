using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_without_zugferd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Work with embedded files – the Attachments collection does not exist.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // Iterate backwards so that removal does not affect the index of remaining items.
                for (int i = doc.EmbeddedFiles.Count - 1; i >= 0; i--)
                {
                    FileSpecification fileSpec = doc.EmbeddedFiles[i];
                    string name = fileSpec.Name ?? string.Empty;

                    // ZUGFeRD files are usually XML and often contain the word "ZUGFeRD" in the name.
                    if (name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        // Delete expects the attachment name (string), not the index.
                        doc.EmbeddedFiles.Delete(name);
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"ZUGFeRD attachment removed. Output saved to '{outputPath}'.");
    }
}
