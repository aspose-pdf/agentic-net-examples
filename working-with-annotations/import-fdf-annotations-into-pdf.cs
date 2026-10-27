using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the FDF file containing form field data
        const string pdfPath = "source.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "merged_output.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // ------------------------------------------------------------
                // Import FDF data using the core API (no Facades).
                // The example assumes a simple text‑based FDF where each line
                // contains "fieldName=fieldValue". For more complex FDF files
                // a full parser would be required.
                // ------------------------------------------------------------
                foreach (var line in File.ReadLines(fdfPath))
                {
                    // Skip empty lines and comments
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                        continue;

                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length != 2)
                        continue; // malformed line – ignore

                    string fieldName = parts[0].Trim();
                    string fieldValue = parts[1].Trim();

                    // Find the field in the document's AcroForm
                    var field = pdfDoc.Form?.Fields?.FirstOrDefault(f =>
                        string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                        string.Equals(f.FullName, fieldName, StringComparison.Ordinal));

                    if (field != null)
                    {
                        // Set the value – the core API stores the value as an object
                        field.Value = fieldValue;
                    }
                }

                // Save the resulting PDF with the merged form data
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"FDF data merged successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
