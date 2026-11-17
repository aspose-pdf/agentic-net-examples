using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing portfolio (embedded) items
        const string inputPath = "input.pdf";
        // Output PDF after removal of matching items
        const string outputPath = "output.pdf";
        // Description text to match for deletion
        const string descriptionToDelete = "Sample Portfolio Item";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the collection of embedded (portfolio) files
                EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

                if (embeddedFiles != null && embeddedFiles.Count > 0)
                {
                    // Iterate backwards because EmbeddedFileCollection uses 1‑based indexing
                    for (int i = embeddedFiles.Count; i >= 1; i--)
                    {
                        FileSpecification fileSpec = embeddedFiles[i];

                        // Compare the Description property (case‑insensitive)
                        if (!string.IsNullOrEmpty(fileSpec.Description) &&
                            string.Equals(fileSpec.Description, descriptionToDelete, StringComparison.OrdinalIgnoreCase))
                        {
                            // Remove the matching embedded file by its name
                            if (!string.IsNullOrEmpty(fileSpec.Name))
                            {
                                embeddedFiles.Delete(fileSpec.Name);
                                Console.WriteLine($"Removed portfolio item with description: \"{fileSpec.Description}\"");
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to \"{outputPath}\"");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
