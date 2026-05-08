using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_zugferd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Identify embedded files whose name contains "ZUGFeRD"
                var namesToDelete = new List<string>();
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (!string.IsNullOrEmpty(fileSpec.Name) &&
                        fileSpec.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        namesToDelete.Add(fileSpec.Name);
                    }
                }

                // Delete the identified ZUGFeRD attachments
                foreach (string name in namesToDelete)
                {
                    doc.EmbeddedFiles.Delete(name);
                }

                // Save the modified PDF, preserving all other content
                doc.Save(outputPath);
            }

            Console.WriteLine($"ZUGFeRD attachment removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
