using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfBatchReplacer
{
    // Ensures that a PDF exists at the given path. If the file is missing, a simple placeholder PDF is created.
    private static void EnsureSamplePdf(string path)
    {
        if (File.Exists(path))
            return;

        // Create a minimal PDF with a single page containing some sample text.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a paragraph so the document is not empty.
            page.Paragraphs.Add(new TextFragment("Sample PDF – generated because the input file was not found."));
            doc.Save(path);
        }
    }

    // Replaces all occurrences of each key in the dictionary with its corresponding value.
    public static void ReplaceStrings(string inputPdfPath, string outputPdfPath, Dictionary<string, string> replacements)
    {
        // Make sure the input PDF exists; create a placeholder if it does not.
        EnsureSamplePdf(inputPdfPath);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over each replacement pair.
            foreach (KeyValuePair<string, string> pair in replacements)
            {
                // Create an absorber that searches for the old text.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(pair.Key);

                // Search the entire document.
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new text.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = pair.Value;
                }
            }

            // Save the modified document as PDF.
            doc.Save(outputPdfPath);
        }
    }

    // Example usage.
    static void Main(string[] args)
    {
        // Allow optional command‑line arguments for input and output paths.
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        var replacements = new Dictionary<string, string>
        {
            { "OldCompany", "NewCompany" },
            { "2022", "2023" },
            { "Confidential", "Public" }
        };

        try
        {
            ReplaceStrings(inputPath, outputPath, replacements);
            Console.WriteLine($"Replacements completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
