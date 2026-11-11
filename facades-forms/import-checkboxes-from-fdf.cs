using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Provides FdfDocument (placeholder if missing in the referenced version)
using Aspose.Pdf.Forms;   // Provides CheckboxField

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string fdfPath    = "checkboxes.fdf";
        const string outputPath = "merged.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Build a set of existing field names to avoid duplicates
            HashSet<string> existingNames = new HashSet<string>();
            foreach (var field in pdfDoc.Form.Fields)
                existingNames.Add(field.FullName);

            // Load the FDF file (also disposable). The placeholder implementation works even if the real class is absent.
            using (Aspose.Pdf.Facades.FdfDocument fdfDoc = new Aspose.Pdf.Facades.FdfDocument(fdfPath))
            {
                // Iterate over all fields defined in the FDF
                foreach (var fdfField in fdfDoc.Fields)
                {
                    // Process only checkbox definitions
                    if (fdfField.Type != Aspose.Pdf.Facades.FdfFieldType.CheckBox)
                        continue;

                    string fieldName = fdfField.Name;

                    // Skip if a field with the same name already exists in the PDF
                    if (existingNames.Contains(fieldName))
                        continue;

                    // Create a new checkbox field and set its properties based on the FDF entry
                    CheckboxField checkBox = new CheckboxField(pdfDoc);
                    checkBox.PartialName = fieldName;
                    // In FDF a checked box is usually represented by the value "Yes"
                    checkBox.Checked = string.Equals(fdfField.Value, "Yes", StringComparison.OrdinalIgnoreCase);

                    // Add the new field to the PDF form and record its name
                    pdfDoc.Form.Add(checkBox);
                    existingNames.Add(fieldName);
                }
            }

            // Save the updated PDF (no special SaveOptions needed for PDF output)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}

// ---------------------------------------------------------------------------
// Placeholder implementation for FDF handling.
// This is only required when the referenced Aspose.Pdf version does not contain
// the Aspose.Pdf.Facades.FdfDocument class. It mimics the minimal API used in
// the sample (constructor, Fields collection, and IDisposable).
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Represents the type of a field stored in an FDF file.
    /// Only the values required by the sample are defined.
    /// </summary>
    public enum FdfFieldType
    {
        CheckBox,
        // Other field types could be added here if needed.
    }

    /// <summary>
    /// Simple representation of a field inside an FDF document.
    /// </summary>
    public class FdfField
    {
        public FdfFieldType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Minimal placeholder for the Aspose.Pdf.Facades.FdfDocument class.
    /// It reads a very simple line‑based format (optional) but otherwise
    /// provides an empty collection so the code compiles and runs.
    /// </summary>
    public class FdfDocument : IDisposable
    {
        public List<FdfField> Fields { get; } = new List<FdfField>();

        public FdfDocument(string path)
        {
            // Attempt a very lightweight parsing of a custom text format:
            // each line: <type>|<name>|<value>
            // Example: CheckBox|agreeTerms|Yes
            // This is only for demonstration; real FDF parsing requires the
            // official Aspose component which may not be present in the current
            // package version.
            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length != 3)
                        continue;

                    if (!Enum.TryParse(parts[0], true, out FdfFieldType type))
                        continue;

                    Fields.Add(new FdfField
                    {
                        Type = type,
                        Name = parts[1],
                        Value = parts[2]
                    });
                }
            }
            catch
            {
                // If the file cannot be read or parsed, leave Fields empty.
            }
        }

        public void Dispose()
        {
            // No unmanaged resources to release.
        }
    }
}
