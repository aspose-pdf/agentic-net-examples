using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

        // Verify that source files exist
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // 1. Import form‑field values from the FDF (if any).
            // ------------------------------------------------------------
            ImportFormFieldsFromFdf(pdfDoc, fdfPath);

            // ------------------------------------------------------------
            // 2. Import page‑level annotations from the FDF.
            // ------------------------------------------------------------
            ImportPageAnnotationsFromFdf(pdfDoc, fdfPath);

            // Save the updated PDF with the imported data
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Reads simple key‑value pairs from an FDF file and assigns matching values to form fields.
    /// Expected line format (case‑insensitive):
    ///   FieldName=MyField;Value=Some text
    /// Lines that do not contain both keys are ignored.
    /// </summary>
    private static void ImportFormFieldsFromFdf(Document doc, string fdfPath)
    {
        if (doc?.Form?.Fields == null)
            return;

        foreach (var line in File.ReadLines(fdfPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => p.Trim())
                            .Where(p => p.Length > 0)
                            .ToArray();
            if (parts.Length == 0)
                continue;

            string fieldName = null;
            string fieldValue = null;
            foreach (var part in parts)
            {
                var kv = part.Split(new[] { '=' }, 2);
                if (kv.Length != 2)
                    continue;
                var key = kv[0].Trim().ToUpperInvariant();
                var val = kv[1].Trim();
                if (key == "FIELDNAME" || key == "NAME")
                    fieldName = val;
                else if (key == "VALUE")
                    fieldValue = val;
            }

            if (fieldName == null || fieldValue == null)
                continue; // not a form‑field line

            var field = doc.Form.Fields.FirstOrDefault(f =>
                string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                string.Equals(f.FullName, fieldName, StringComparison.Ordinal));
            if (field != null)
                field.Value = fieldValue;
        }
    }

    /// <summary>
    /// Reads simple annotation definitions from an FDF file and adds them to the appropriate pages.
    /// Expected line format (case‑insensitive):
    ///   Page=2;Type=Text;Rect=100,200,150,250;Contents=Note text
    /// Supported Types: Text (TextAnnotation), Highlight (HighlightAnnotation).
    /// </summary>
    private static void ImportPageAnnotationsFromFdf(Document doc, string fdfPath)
    {
        foreach (var line in File.ReadLines(fdfPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var dict = ParseKeyValuePairs(line);
            if (!dict.ContainsKey("PAGE") || !dict.ContainsKey("TYPE") || !dict.ContainsKey("RECT"))
                continue; // not an annotation line

            if (!int.TryParse(dict["PAGE"], out int pageNumber) || pageNumber < 1 || pageNumber > doc.Pages.Count)
                continue; // invalid page number

            var rectParts = dict["RECT"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (rectParts.Length != 4)
                continue; // malformed rectangle

            if (!double.TryParse(rectParts[0], out double llx) ||
                !double.TryParse(rectParts[1], out double lly) ||
                !double.TryParse(rectParts[2], out double urx) ||
                !double.TryParse(rectParts[3], out double ury))
                continue; // invalid rectangle values

            var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            var type = dict["TYPE"].Trim().ToUpperInvariant();
            Annotation annotation = null;

            switch (type)
            {
                case "TEXT":
                    var textAnnot = new TextAnnotation(doc.Pages[pageNumber], rect);
                    if (dict.TryGetValue("CONTENTS", out string contents))
                        textAnnot.Contents = contents;
                    annotation = textAnnot;
                    break;

                case "HIGHLIGHT":
                    var highlight = new HighlightAnnotation(doc.Pages[pageNumber], rect);
                    if (dict.TryGetValue("CONTENTS", out string hlContents))
                        highlight.Contents = hlContents;
                    annotation = highlight;
                    break;

                // Add more cases here for other annotation types as needed.
                default:
                    // Unsupported annotation type – skip.
                    continue;
            }

            if (annotation != null)
                doc.Pages[pageNumber].Annotations.Add(annotation);
        }
    }

    /// <summary>
    /// Helper that converts a semicolon‑separated list of key=value pairs into a dictionary.
    /// Keys are stored in upper‑case to make look‑ups case‑insensitive.
    /// </summary>
    private static Dictionary<string, string> ParseKeyValuePairs(string line)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            var kv = part.Split(new[] { '=' }, 2);
            if (kv.Length != 2)
                continue;
            var key = kv[0].Trim().ToUpperInvariant();
            var value = kv[1].Trim();
            dict[key] = value;
        }
        return dict;
    }
}
