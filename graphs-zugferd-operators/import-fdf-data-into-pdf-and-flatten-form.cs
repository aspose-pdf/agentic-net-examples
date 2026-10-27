using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "filled_flattened.pdf";

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
            var doc = new Document(pdfPath);

            // Populate form fields from FDF
            PopulateFromFdf(doc, fdfPath);

            // Flatten the form so fields become static content
            doc.Form?.Flatten();

            // Save the resulting PDF
            doc.Save(outputPath);
            Console.WriteLine($"Form filled and flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void PopulateFromFdf(Document doc, string fdfPath)
    {
        // Very simple FDF parser – expects lines like: FieldName=Value
        foreach (var line in File.ReadLines(fdfPath))
        {
            // Skip empty lines and comment lines
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("%") || line.TrimStart().StartsWith("#"))
                continue;

            // Split on the first '=' character
            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                continue;

            var name = parts[0].Trim();
            var value = parts[1].Trim();

            var field = doc.Form?.Fields?.FirstOrDefault(f =>
                string.Equals(f.Name, name, StringComparison.Ordinal) ||
                string.Equals(f.FullName, name, StringComparison.Ordinal));

            if (field != null)
                field.Value = value;
        }
    }
}