using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";
        // Index of the embedded file to remove (0‑based for the collection enumeration)
        int indexToRemove = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (portfolio) inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files
            var embeddedFiles = doc.EmbeddedFiles;

            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF portfolio.");
            }
            else
            {
                // Validate the requested index
                if (indexToRemove < 0 || indexToRemove >= embeddedFiles.Count)
                {
                    Console.WriteLine($"Index {indexToRemove} is out of range. Embedded file count: {embeddedFiles.Count}");
                }
                else
                {
                    // Retrieve the key (file name) at the specified index
                    string key = embeddedFiles.Keys.ElementAt(indexToRemove);
                    // Delete the embedded file by its key
                    embeddedFiles.DeleteByKey(key);
                    Console.WriteLine($"Deleted embedded file '{key}' at index {indexToRemove}.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}