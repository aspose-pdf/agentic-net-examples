using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchKeywordReplacer
{
    static void Main()
    {
        // Input PDF, output PDF and configuration file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "replacements.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load replacement mappings (format: oldString=newString per line)
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var line in File.ReadAllLines(configPath))
        {
            // Skip empty or comment lines
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                continue; // malformed line – ignore

            var oldText = parts[0];
            var newText = parts[1];
            replacements[oldText] = newText;
        }

        if (replacements.Count == 0)
        {
            Console.WriteLine("No valid replacement entries found.");
            return;
        }

        // Open the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over each replacement pair
            foreach (var kvp in replacements)
            {
                string oldValue = kvp.Key;
                string newValue = kvp.Value;

                // Create an absorber that searches for the current old value
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldValue);

                // Accept the absorber for all pages
                doc.Pages.Accept(absorber);

                // Replace every found fragment with the new value
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = newValue;
                }
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPdfPath}'.");
    }
}