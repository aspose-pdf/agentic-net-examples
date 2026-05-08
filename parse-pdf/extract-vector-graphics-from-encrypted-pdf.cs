using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Encrypted PDF file
        const string password  = "user123";        // Password to open the PDF
        const string outputDir = "VectorGraphics"; // Folder for extracted SVG files

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
                // Decrypt the document so that further operations are allowed
                doc.Decrypt();

                // Configure SVG save options (no SplitIntoPages property – default behavior saves a single SVG file)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG; Aspose.Pdf will create a single SVG file containing all pages.
                string outputPath = Path.Combine(outputDir, "page.svg");
                doc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"Vector graphics have been extracted to SVG file in the '{outputDir}' folder.");
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
