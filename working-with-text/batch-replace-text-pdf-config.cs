using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string configPath = "replace.cfg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load old‑new string mappings from the configuration file
        Dictionary<string, string> replacements = LoadReplacements(configPath);
        if (replacements.Count == 0)
        {
            Console.WriteLine("No replacements found in configuration file.");
            return;
        }

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Apply each replacement across all pages
            foreach (KeyValuePair<string, string> kvp in replacements)
            {
                string oldText = kvp.Key;
                string newText = kvp.Value;

                // Iterate through pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Search for the current old text on the page
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
                    doc.Pages[pageNum].Accept(absorber);

                    // Replace every occurrence found
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = newText;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Batch replacement completed. Saved to '{outputPdf}'.");
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
                continue; // Skip empty or comment lines

            int sepIndex = line.IndexOf('=');
            if (sepIndex <= 0)
                continue; // Invalid line, ignore

            string oldValue = line.Substring(0, sepIndex).Trim();
            string newValue = line.Substring(sepIndex + 1).Trim();

            if (!map.ContainsKey(oldValue))
                map.Add(oldValue, newValue);
        }
        return map;
    }
}