using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchStringReplacer
{
    static void Main()
    {
        // NOTE: To avoid known vulnerabilities in System.Security.Cryptography.Pkcs (v9.0.0),
        // add the following PackageReference to the .csproj file (or update the existing one):
        // <PackageReference Include="System.Security.Cryptography.Pkcs" Version="8.0.0" />
        const string inputFolder = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        // Validate input folder existence before proceeding
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder \"{inputFolder}\" does not exist.");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Define the old‑new string pairs
        var replacements = new Dictionary<string, string>
        {
            { "OldString1", "NewString1" },
            { "OldString2", "NewString2" },
            { "Confidential", "Public" }
            // Add more pairs as needed
        };

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Perform all replacements on the document using TextFragmentAbsorber
                    foreach (KeyValuePair<string, string> pair in replacements)
                    {
                        // Absorb all occurrences of the old text (case‑sensitive search)
                        var absorber = new TextFragmentAbsorber(pair.Key)
                        {
                            TextSearchOptions = new TextSearchOptions(true)
                        };
                        doc.Pages.Accept(absorber);

                        // Replace each found fragment with the new text
                        foreach (TextFragment fragment in absorber.TextFragments)
                        {
                            fragment.Text = pair.Value;
                        }
                    }

                    // Build the output file path and save the modified PDF
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {outputFolder}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process \"{pdfPath}\": {ex.Message}");
            }
        }

        Console.WriteLine("Batch replacement completed.");
    }
}
