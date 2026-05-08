using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Resolve input and output folders relative to the current working directory.
        // This works on Windows, macOS and Linux without mixing path separators.
        string baseDir = Environment.CurrentDirectory;
        string inputFolder = Path.GetFullPath(Path.Combine(baseDir, "InputPdfs"));
        string outputFolder = Path.GetFullPath(Path.Combine(baseDir, "OutputPdfs"));

        // Define old‑>new string replacements
        var replacements = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "OldCompanyName", "NewCompanyName" },
            { "2022", "2023" },
            { "Confidential", "Public" }
        };

        // Ensure input folder exists – if it does not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Apply each replacement pair
                foreach (var kvp in replacements)
                {
                    // Search for the old text
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(kvp.Key);
                    // Search across all pages
                    doc.Pages.Accept(absorber);

                    // Replace each occurrence with the new text
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = kvp.Value;
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName} → {outputPath}");
        }

        Console.WriteLine("Batch replacement completed.");
    }
}
