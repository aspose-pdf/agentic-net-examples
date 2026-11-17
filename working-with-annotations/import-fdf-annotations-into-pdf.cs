using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "output.pdf";

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
            // Load the existing PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Manually import FDF data and assign values to matching form fields
                FillFormFromFdf(pdfDoc, fdfPath);

                // Save the updated PDF; Document.Save without options writes PDF format
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"FDF data merged and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a simple FDF file and assigns the extracted values to the corresponding form fields.
    /// This implementation works for standard FDF entries of the form "/T (fieldName) /V (value)".
    /// </summary>
    /// <param name="doc">The PDF document whose form will be populated.</param>
    /// <param name="fdfPath">Path to the FDF file.</param>
    private static void FillFormFromFdf(Document doc, string fdfPath)
    {
        if (doc?.Form?.Fields == null)
            return;

        string fdfContent = File.ReadAllText(fdfPath);
        // Regex captures field name (/T) and its value (/V). It tolerates whitespace and line‑breaks.
        var matches = Regex.Matches(fdfContent, @"/T\s*\(([^)]+)\)\s*/V\s*\(([^)]+)\)", RegexOptions.Singleline);
        foreach (Match match in matches)
        {
            string fieldName = match.Groups[1].Value;
            string fieldValue = match.Groups[2].Value;

            var field = doc.Form.Fields.FirstOrDefault(f =>
                string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                string.Equals(f.FullName, fieldName, StringComparison.Ordinal));

            if (field != null)
            {
                field.Value = fieldValue;
            }
        }
    }
}
