using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_zugferd.pdf";

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
                // Collect the names of embedded files that contain "ZUGFeRD"
                List<string> namesToRemove = new List<string>();
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    bool isZugferd = (!string.IsNullOrEmpty(fileSpec.Name) &&
                                      fileSpec.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0) ||
                                      (!string.IsNullOrEmpty(fileSpec.Description) &&
                                      fileSpec.Description.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0);
                    if (isZugferd)
                    {
                        namesToRemove.Add(fileSpec.Name);
                    }
                }

                // Delete the identified embedded files
                foreach (string name in namesToRemove)
                {
                    doc.EmbeddedFiles.Delete(name);
                }

                // Save the PDF without the ZUGFeRD attachment
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
