using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input PDF exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the phrases to replace: key = text to find, value = replacement text
        var replacements = new Dictionary<string, string>
        {
            { "Old Phrase 1", "New Phrase A" },
            { "Sample text", "Replaced text" }
            // Add additional pairs as needed
        };

        try
        {
            // Load the PDF document. The Aspose.Pdf assembly must be referenced (e.g., via NuGet).
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each phrase pair
                foreach (var pair in replacements)
                {
                    string searchText  = pair.Key;
                    string replacement = pair.Value;

                    // Locate all occurrences of the search text
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                    doc.Pages.Accept(absorber);

                    // Replace each found fragment while keeping its original TextState
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Changing the Text property preserves the original formatting (font, size, color, etc.).
                        fragment.Text = replacement;
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All replacements applied. Output saved to '{outputPath}'.");
        }
        catch (FileNotFoundException ex) when (ex.FileName != null && ex.FileName.EndsWith("Aspose.PDF.dll", StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine("The Aspose.PDF.dll assembly could not be found. Ensure the Aspose.Pdf NuGet package is installed and the assembly is available at runtime.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
