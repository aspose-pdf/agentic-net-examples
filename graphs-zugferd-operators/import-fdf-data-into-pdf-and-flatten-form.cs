using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
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
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Import FDF data manually (Aspose.Pdf core API does not expose ImportFdf)
                FillFormFromFdf(doc, fdfPath);

                // Flatten the form to make fields read‑only
                doc.Form?.Flatten();

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form populated and flattened. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a simple FDF file and assigns the extracted values to matching form fields.
    /// This implementation handles the most common FDF syntax: /T (fieldName) /V (value).
    /// </summary>
    private static void FillFormFromFdf(Document doc, string fdfPath)
    {
        string fdfContent = File.ReadAllText(fdfPath);
        // Regex captures field name and value inside parentheses after /T and /V tokens.
        var matches = Regex.Matches(fdfContent, @"/T\s*\(([^)]+)\)\s*/V\s*\(([^)]+)\)", RegexOptions.Singleline);
        foreach (Match match in matches)
        {
            string fieldName = match.Groups[1].Value;
            string fieldValue = match.Groups[2].Value;

            // Find the field in the document (match by FullName or Name)
            var field = doc.Form?.Fields?.FirstOrDefault(f =>
                string.Equals(f.FullName, fieldName, StringComparison.Ordinal) ||
                string.Equals(f.Name, fieldName, StringComparison.Ordinal));

            if (field != null)
            {
                field.Value = fieldValue;
            }
        }
    }
}
