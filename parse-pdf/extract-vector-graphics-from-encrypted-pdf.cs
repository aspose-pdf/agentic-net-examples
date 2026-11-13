using System;
using System.IO;
using Aspose.Pdf;

class ExtractVectorGraphics
{
    static void Main()
    {
        // Paths and password – adjust as needed
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string password = "userPassword";
        const string outputFolder = "VectorGraphics";

        // Verify input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Open the encrypted PDF by providing the password
            using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(encryptedPdfPath, password))
            {
                // Iterate using 1‑based page indexing
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    // Create a new document containing only the current page
                    using (Aspose.Pdf.Document singlePageDoc = new Aspose.Pdf.Document())
                    {
                        // Add the page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Define SVG save options (default options are sufficient for vector extraction)
                        Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();

                        // Build output file name
                        string outputPath = Path.Combine(outputFolder, $"Page_{i}.svg");

                        // Save the single‑page document as SVG (vector graphics)
                        singlePageDoc.Save(outputPath, svgOptions);

                        Console.WriteLine($"Extracted vector graphics for page {i} to '{outputPath}'.");
                    }
                }
            }

            Console.WriteLine("Vector graphics extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}