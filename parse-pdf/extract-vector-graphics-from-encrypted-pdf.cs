using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Encrypted PDF file
        const string password  = "user123";        // Password for opening the PDF
        const string outputDir = "ExtractedVectors"; // Folder for SVG files

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF by providing the password
            using (Document doc = new Document(inputPath, password))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    string svgPath = Path.Combine(outputDir, $"Page_{i}.svg");

                    // Create a temporary document containing only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page reference; this does not copy the whole source document
                        singlePageDoc.Pages.Add(doc.Pages[i]);

                        // Save the single page as SVG to capture vector graphics
                        SvgSaveOptions svgOptions = new SvgSaveOptions();
                        singlePageDoc.Save(svgPath, svgOptions);
                    }
                }
            }

            Console.WriteLine("Vector graphics extracted to SVG files successfully.");
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