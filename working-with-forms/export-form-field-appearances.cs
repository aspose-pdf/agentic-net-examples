using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "FieldAppearances";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF containing form fields
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Iterate over each form field (Field) in the document
                foreach (Field field in srcDoc.Form)
                {
                    // Create a new single‑page PDF to hold the field's appearance
                    using (Document appearanceDoc = new Document())
                    {
                        // Add a blank page – Aspose.Pdf does not add one automatically for a new Document
                        appearanceDoc.Pages.Add();

                        // Copy the field's appearance onto page 1 at the same rectangle
                        // The rectangle is taken from the original field
                        appearanceDoc.Form.AddFieldAppearance(field, 1, field.Rect);

                        // Build a safe file name from the field's full name
                        string safeName = MakeFileSystemSafeName(field.FullName);
                        string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");

                        // Save the appearance PDF
                        appearanceDoc.Save(outputPath);
                        Console.WriteLine($"Exported appearance for field '{field.FullName}' to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to replace characters that are invalid in file names
    static string MakeFileSystemSafeName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return string.IsNullOrWhiteSpace(name) ? "UnnamedField" : name;
    }
}
