using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "replacements.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load replacement map from JSON file: { "old1": "new1", "old2": "new2", ... }
        Dictionary<string, string> replacements;
        try
        {
            string json = File.ReadAllText(configPath);
            replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (replacements == null || replacements.Count == 0)
            {
                Console.Error.WriteLine("Replacement map is empty.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        try
        {
            // Load PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate over each page (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Apply each replacement on the current page
                    foreach (var kvp in replacements)
                    {
                        string oldText = kvp.Key;
                        string newText = kvp.Value;

                        // Search for the old text
                        TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
                        page.Accept(absorber);

                        // Replace all occurrences found
                        foreach (TextFragment fragment in absorber.TextFragments)
                        {
                            fragment.Text = newText;
                        }
                    }
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Replacements applied. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Processing error: {ex.Message}");
        }
    }
}
