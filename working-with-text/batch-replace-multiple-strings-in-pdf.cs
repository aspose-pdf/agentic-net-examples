using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchStringReplacer
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\Pdf\Input";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Dictionary of old string => new string replacements
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "OldCompany", "NewCompany" },
            { "Confidential", "Public" },
            { "2022", "2023" }
            // add more pairs as needed
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load PDF using the core Document API (no Facades)
            using (Document doc = new Document(inputPath))
            {
                // Absorb all text fragments in the document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                doc.Pages.Accept(absorber);

                // Iterate over each fragment and apply all replacements
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    string text = fragment.Text;
                    foreach (var kvp in replacements)
                    {
                        if (text.Contains(kvp.Key))
                        {
                            text = text.Replace(kvp.Key, kvp.Value);
                        }
                    }
                    fragment.Text = text;
                }

                // Save the modified PDF (same format, so no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("Batch replacement completed.");
    }
}
