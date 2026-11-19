using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "replacements.json";

        // Validate required files
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

        // Load replacement map from JSON: { "old1": "new1", "old2": "new2", ... }
        Dictionary<string, string> replacements;
        try
        {
            replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configPath));
            if (replacements == null || replacements.Count == 0)
            {
                Console.Error.WriteLine("Configuration file is empty or invalid.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Open the PDF – wrapped in using for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Apply each replacement on the current page
                foreach (KeyValuePair<string, string> kvp in replacements)
                {
                    // Search for the old string
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(kvp.Key);
                    page.Accept(absorber);

                    // Replace every found fragment with the new string
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = kvp.Value;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPdfPath}'.");
    }
}