using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the FDF file and the output PDF
        string pdfPath = "input.pdf";
        string fdfPath = Path.Combine("Resources", "annotations.fdf");
        string outputPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // ------------------------------------------------------------
                // Import annotations (form field values) from the FDF file.
                // Aspose.Pdf's core API does not expose an ImportFdf method, so
                // we read the FDF file manually and assign values to matching
                // fields in the document's Form.Fields collection.
                // ------------------------------------------------------------
                foreach (var line in File.ReadLines(fdfPath))
                {
                    // Skip empty lines and comments (lines starting with '#')
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                        continue;

                    // FDF entries are typically "fieldName=value"
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length != 2)
                        continue; // malformed line – ignore

                    string fieldName = parts[0].Trim();
                    string fieldValue = parts[1].Trim();

                    // Find the field by Name or FullName (covers hierarchical names)
                    var field = doc.Form?.Fields?.FirstOrDefault(f =>
                        string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                        string.Equals(f.FullName, fieldName, StringComparison.Ordinal));

                    if (field != null)
                    {
                        field.Value = fieldValue;
                    }
                }

                // Example processing: enumerate annotations on each page
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based page indexing
                {
                    Page page = doc.Pages[i];
                    Console.WriteLine($"Page {i} contains {page.Annotations.Count} annotation(s).");
                    foreach (var annotation in page.Annotations)
                    {
                        Console.WriteLine($"  - {annotation.GetType().Name}");
                    }
                }

                // Save the modified PDF with the imported annotations
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
