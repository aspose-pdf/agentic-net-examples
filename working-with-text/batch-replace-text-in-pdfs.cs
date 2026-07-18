using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchReplace
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Define old‑>new string pairs
        var replacements = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "OldCompanyName", "NewCompanyName" },
            { "2022", "2023" },
            { "Confidential", "Public" }
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Open the PDF document inside a using block (ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each replacement pair
                foreach (var pair in replacements)
                {
                    // Create an absorber that searches for the old text
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(pair.Key);
                    // Search the whole document
                    doc.Pages.Accept(absorber);

                    // Replace each found fragment with the new text
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = pair.Value;
                    }
                }

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("Batch replacement completed.");
    }
}