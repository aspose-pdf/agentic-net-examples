using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "replace.cfg";

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

        // Load replacement mappings from the configuration file.
        Dictionary<string, string> replacements = LoadReplacements(configPath);
        if (replacements.Count == 0)
        {
            Console.WriteLine("No replacement entries found in the configuration file.");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through each page (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Apply all replacements on the current page.
                foreach (KeyValuePair<string, string> kvp in replacements)
                {
                    // Search for the old phrase.
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(kvp.Key);
                    page.Accept(absorber);

                    // Replace every occurrence found.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = kvp.Value;
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPdfPath}'.");
    }

    // Reads a simple key=value configuration file.
    // Lines starting with '#' are treated as comments.
    static Dictionary<string, string> LoadReplacements(string path)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (string rawLine in File.ReadAllLines(path))
        {
            string line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith("#"))
                continue; // Skip empty or comment lines.

            int separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
                continue; // Invalid line; ignore.

            string oldValue = line.Substring(0, separatorIndex).Trim();
            string newValue = line.Substring(separatorIndex + 1).Trim();

            if (!string.IsNullOrEmpty(oldValue))
                map[oldValue] = newValue;
        }
        return map;
    }
}