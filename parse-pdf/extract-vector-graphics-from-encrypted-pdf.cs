using System;
using System.IO;
using Aspose.Pdf; // Save option classes like SvgSaveOptions are now in the root Aspose.Pdf namespace

class ExtractVectorGraphics
{
    static void Main()
    {
        // Input encrypted PDF path and password
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string userPassword     = "user123";

        // Output folder for extracted SVG files (vector graphics)
        const string outputFolder = "ExtractedVectors";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Open the encrypted PDF by providing the password to the Document constructor
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    // Create a temporary document containing only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page to the new document (the page object is transferred)
                        singlePageDoc.Pages.Add(doc.Pages[pageIndex]);

                        // Define SVG save options – this preserves vector information
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output file name (e.g., Page_1.svg)
                        string outputSvgPath = Path.Combine(outputFolder, $"Page_{pageIndex}.svg");

                        // Save the single‑page document as SVG (vector graphics)
                        singlePageDoc.Save(outputSvgPath, svgOptions);

                        Console.WriteLine($"Extracted vector graphics from page {pageIndex} to '{outputSvgPath}'.");
                    }
                }
            }

            Console.WriteLine("Vector graphics extraction completed successfully.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
