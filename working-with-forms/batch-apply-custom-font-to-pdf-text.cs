using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Network folders – adjust these paths as needed
        const string sourceFolder = @"\\network\share\pdfs";
        const string outputFolder = @"\\network\share\output";

        // Path to the custom TrueType font file
        const string customFontPath = @"C:\Fonts\CustomFont.ttf";

        // Verify source folder exists
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Load the custom font and mark it for embedding
        Font customFont = FontRepository.OpenFont(customFontPath);
        customFont.IsEmbedded = true;

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_custom.pdf");

                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Create an absorber that captures all text fragments in the document
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();

                    // Search the entire document (all pages)
                    doc.Pages.Accept(absorber);

                    // Apply the custom font to every captured text fragment
                    absorber.ApplyForAllFragments(customFont);

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}