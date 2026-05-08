using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Network folder containing the PDF files
        const string sourceFolder = @"\\NetworkShare\PdfFolder";
        // Optional: folder to write the updated PDFs (can be the same as sourceFolder)
        const string outputFolder = @"\\NetworkShare\PdfFolder\Processed";

        // Path to the custom TrueType font file to be applied
        const string customFontPath = @"C:\Fonts\CustomFont.ttf";

        // Verify that the font file exists
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // Load the custom font
        Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(customFontPath);
        // Ensure the font will be embedded in the output PDFs
        customFont.IsEmbedded = true;

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Create a TextFragmentAbsorber that captures all text fragments
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();

                    // Absorb text from all pages
                    doc.Pages.Accept(absorber);

                    // Apply the custom font (and keep existing font size) to every absorbed fragment
                    absorber.ApplyForAllFragments(customFont);

                    // Determine output file path
                    string fileName = Path.GetFileName(pdfPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified document (overwrites if same folder is used)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch font application completed.");
    }
}