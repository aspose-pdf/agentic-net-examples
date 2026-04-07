using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchStringReplacer
{
    // Replaces all occurrences of each key in the replacements dictionary with its corresponding value
    // in every PDF file found in the input directory. The modified PDFs are saved to the output directory
    // preserving the original file names.
    public static void ProcessFolder(string inputFolder, string outputFolder, Dictionary<string, string> replacements)
    {
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (using the recommended lifecycle rule)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over each replacement pair
                    foreach (KeyValuePair<string, string> pair in replacements)
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

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }

    // Example usage
    static void Main()
    {
        // Define the folder containing PDFs to process
        string inputFolder = @"C:\PdfInput";

        // Define the folder where processed PDFs will be saved
        string outputFolder = @"C:\PdfOutput";

        // Define the strings to replace: old => new
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "OldCompanyName", "NewCompanyName" },
            { "2022", "2023" },
            { "Confidential", "Public" }
        };

        ProcessFolder(inputFolder, outputFolder, replacements);
    }
}