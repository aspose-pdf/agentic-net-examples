using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchKeywordReplacer
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string configPath   = "replacements.json"; // {"old1":"new1","old2":"new2",...}
        const string outputPdfPath = "output.pdf";

        // Validate files
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

        // Load replacement map from JSON file
        Dictionary<string, string> replacements;
        try
        {
            string json = File.ReadAllText(configPath);
            replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (replacements == null || replacements.Count == 0)
            {
                Console.Error.WriteLine("No replacements found in configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Apply each replacement on the current page
                foreach (KeyValuePair<string, string> kvp in replacements)
                {
                    // Create an absorber that searches for the old phrase
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(kvp.Key);

                    // Accept the absorber for the current page
                    page.Accept(absorber);

                    // Replace every found fragment with the new text
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = kvp.Value;
                    }
                }
            }

            // Save the modified document (lifecycle rule: use Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPdfPath}'.");
    }
}