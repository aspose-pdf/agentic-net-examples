using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "checkboxes.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create a simple FDF writer (the Aspose.FdfDocument class is not available in the referenced version)
            SimpleFdfWriter fdfWriter = new SimpleFdfWriter();

            // Iterate over all form fields in the PDF
            foreach (Field field in pdfDoc.Form)
            {
                // Process only checkbox fields
                if (field is CheckboxField checkBox)
                {
                    // Add the checkbox field definition to the FDF (no value assigned)
                    fdfWriter.AddField(checkBox.FullName, string.Empty);
                }
            }

            // Save the FDF file containing only checkbox definitions
            fdfWriter.Save(fdfPath);
        }

        Console.WriteLine($"Checkbox definitions exported to '{fdfPath}'.");
    }
}

/// <summary>
/// Minimal FDF writer that creates a valid FDF file containing field definitions.
/// This replaces the missing Aspose.Pdf.Facades.FdfDocument class.
/// </summary>
public class SimpleFdfWriter
{
    private readonly List<(string Name, string Value)> _fields = new();

    /// <summary>
    /// Adds a field definition to the FDF.
    /// </summary>
    /// <param name="name">The fully‑qualified field name.</param>
    /// <param name="value">The field value (empty string for definition only).</param>
    public void AddField(string name, string value)
    {
        _fields.Add((name, value));
    }

    /// <summary>
    /// Writes the accumulated fields to an FDF file.
    /// </summary>
    /// <param name="path">The output file path.</param>
    public void Save(string path)
    {
        using var writer = new StreamWriter(path, false, System.Text.Encoding.ASCII);
        writer.WriteLine("%FDF-1.2");
        writer.WriteLine("1 0 obj");
        writer.WriteLine("<< /FDF << /Fields [");
        foreach (var (name, value) in _fields)
        {
            writer.WriteLine($"<< /T ({Escape(name)}) /V ({Escape(value)}) >>");
        }
        writer.WriteLine("] >> >>");
        writer.WriteLine("endobj");
        writer.WriteLine("trailer");
        writer.WriteLine("<< /Root 1 0 R >>");
        writer.WriteLine("%%EOF");
    }

    private static string Escape(string s)
    {
        // Escape backslashes and parentheses as required by the PDF syntax
        return s.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
    }
}