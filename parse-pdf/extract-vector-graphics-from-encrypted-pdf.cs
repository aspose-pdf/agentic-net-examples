using System;
using System.IO;
using Aspose.Pdf;

class ExtractVectorGraphics
{
    static void Main()
    {
        // Input encrypted PDF and password
        const string inputPdf = "encrypted_input.pdf";
        const string password = "userPassword";
        const string outputDir = "ExtractedVectors";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF by providing the password to the constructor
            using (Document doc = new Document(inputPdf, password))
            {
                // Optional: decrypt the document so that further operations are unrestricted
                doc.Decrypt();

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Prepare SVG save options – default options are sufficient for a single page
                    var svgOpts = new SvgSaveOptions();

                    // Build output file name for the current page
                    string svgPath = Path.Combine(outputDir, $"Page_{i}.svg");

                    // Save only the current page as SVG by creating a temporary single‑page document
                    using (Document singlePageDoc = new Document())
                    {
                        // Add a copy of the page to the temporary document
                        singlePageDoc.Pages.Add(doc.Pages[i]);
                        singlePageDoc.Save(svgPath, svgOpts);
                    }

                    Console.WriteLine($"Extracted vector graphics from page {i} → {svgPath}");
                }
            }

            Console.WriteLine("Vector graphic extraction completed.");
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