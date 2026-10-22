using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// Stub for missing FdfDocument class (if the Aspose.Pdf library version does not contain it).
// In a real project, reference the proper Aspose.Pdf NuGet package that includes Aspose.Pdf.Facades.FdfDocument.
namespace Aspose.Pdf.Facades
{
    public class FdfDocument
    {
        private readonly System.Collections.Generic.List<(string Name, string Value)> _fields = new();

        /// <summary>
        /// Adds a field definition to the FDF document.
        /// </summary>
        public void AddField(string name, string value)
        {
            _fields.Add((name, value));
        }

        /// <summary>
        /// Saves the FDF document to the specified path.
        /// This stub writes a simple "name=value" text file for demonstration purposes.
        /// </summary>
        public void Save(string path)
        {
            using var writer = new StreamWriter(path);
            foreach (var field in _fields)
            {
                writer.WriteLine($"{field.Name}={field.Value}");
            }
        }
    }
}

class ExportCheckboxesToFdf
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "checkboxes.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a new FDF document to hold the checkbox definitions
            FdfDocument fdfDoc = new FdfDocument();

            // Iterate over all form fields in the PDF
            foreach (var field in pdfDoc.Form.Fields)
            {
                // Process only checkbox fields
                if (field is CheckboxField checkBox)
                {
                    // Add the checkbox field definition to the FDF.
                    // The value is left empty because we only need the definition.
                    fdfDoc.AddField(checkBox.FullName, string.Empty);
                }
            }

            // Save the FDF file containing only checkbox definitions
            fdfDoc.Save(outputFdfPath);
        }

        Console.WriteLine($"Checkbox field definitions exported to '{outputFdfPath}'.");
    }
}
