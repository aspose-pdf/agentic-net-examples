using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchKeywordReplacer
{
    // Reads a simple CSV configuration file where each line is: oldString,newString
    static Dictionary<string, string> LoadReplacementMap(string configPath)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var line in File.ReadAllLines(configPath))
        {
            // Skip empty lines or comment lines
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
                continue; // malformed line – ignore

            string oldText = parts[0].Trim();
            string newText = parts[1].Trim();
            if (!map.ContainsKey(oldText))
                map.Add(oldText, newText);
        }
        return map;
    }

    static void ReplaceKeywords(string inputPdf, string outputPdf, string configFile)
    {
        var replacements = LoadReplacementMap(configFile);
        if (replacements.Count == 0)
        {
            Console.WriteLine("No replacement pairs found in the configuration file.");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // For each replacement pair, search and replace on the current page
                foreach (var kvp in replacements)
                {
                    string oldPhrase = kvp.Key;
                    string newPhrase = kvp.Value;

                    // Create an absorber that looks for the current old phrase
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldPhrase);
                    // Accept the absorber for the current page
                    page.Accept(absorber);

                    // Replace every occurrence found on this page
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = newPhrase;
                    }
                }
            }

            // Save the modified document (no SaveOptions needed because we output PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Keyword replacement completed. Output saved to '{outputPdf}'.");
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "replacements.csv";

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

        try
        {
            ReplaceKeywords(inputPdfPath, outputPdfPath, configPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}