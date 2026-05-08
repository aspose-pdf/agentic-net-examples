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
        const string configPath = "replacements.txt";

        // Validate input files
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
        Dictionary<string, string> replacements = LoadMappings(configPath);

        // Open the PDF document (lifecycle: using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over each mapping and replace occurrences page by page
            foreach (KeyValuePair<string, string> pair in replacements)
            {
                string oldText = pair.Key;
                string newText = pair.Value;

                // Process every page (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Search for the current old text on the page
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
                    doc.Pages[pageNum].Accept(absorber);

                    // Replace each found fragment with the new text
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = newText;
                    }
                }
            }

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPdf}'.");
    }

    // Reads a simple key=value configuration file and returns a dictionary
    static Dictionary<string, string> LoadMappings(string path)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (string line in File.ReadAllLines(path))
        {
            // Ignore empty lines and comments (lines starting with '#')
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            // Split on the first '=' character
            string[] parts = line.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                string oldValue = parts[0].Trim();
                string newValue = parts[1].Trim();

                // Avoid duplicate keys
                if (!map.ContainsKey(oldValue))
                    map.Add(oldValue, newValue);
            }
        }
        return map;
    }
}