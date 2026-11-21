using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string xmpKey = "dc:creator"; // XMP property name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the metadata dictionary (contains XMP entries)
            var metadata = doc.Metadata;

            // Retrieve the existing value; if the key does not exist, treat it as empty
            string currentValue = metadata.ContainsKey(xmpKey) ? metadata[xmpKey]?.ToString() ?? string.Empty : string.Empty;

            // Modify the value as needed (example: append a suffix)
            string updatedValue = currentValue + " - Modified";

            // Write the updated value back into the metadata dictionary
            metadata[xmpKey] = updatedValue;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
