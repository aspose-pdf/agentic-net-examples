using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";
        // Index of the embedded file to remove (0‑based)
        int indexToRemove = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF portfolio
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Aspose uses 1‑based indexing for EmbeddedFileCollection
            int aspIndex = indexToRemove + 1;

            // Validate the requested index
            if (aspIndex < 1 || aspIndex > embeddedFiles.Count)
            {
                Console.Error.WriteLine("Invalid index for embedded file collection.");
                return;
            }

            // Retrieve the FileSpecification at the requested position
            FileSpecification spec = embeddedFiles[aspIndex];
            string fileName = spec?.Name;

            if (!string.IsNullOrEmpty(fileName))
            {
                // Delete the embedded file by its name
                embeddedFiles.Delete(fileName);
                Console.WriteLine($"Deleted embedded file '{fileName}' at index {indexToRemove}.");
            }
            else
            {
                Console.Error.WriteLine("Failed to obtain the file name of the embedded file.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
