using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to resources
        string resourcesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");
        string pdfPath = Path.Combine(resourcesFolder, "input.pdf");
        string fdfPath = Path.Combine(resourcesFolder, "annotations.fdf");
        string outputPath = Path.Combine(resourcesFolder, "output.pdf");

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

        // Load PDF document with deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Import annotations / form data from the FDF file manually (Form.ImportFdf does not exist)
            FillFormFromFdf(pdfDoc, fdfPath);

            // Example processing: enumerate imported annotations (form fields are also annotations)
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Use AnnotationType (base API) instead of the non‑existent Subtype property
                    var type = annot.AnnotationType;

                    // Title exists only on markup‑type annotations; cast safely
                    string title = string.Empty;
                    if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                    {
                        title = markup.Title;
                    }

                    Console.WriteLine($"Page {page.Number}, Type {type}, Title {title}, Contents {annot.Contents}");
                }
            }

            // Save the PDF with imported annotations
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to: {outputPath}");
    }

    /// <summary>
    /// Parses a simple FDF file and assigns the extracted values to matching form fields.
    /// This replaces the non‑existent Form.ImportFdf method.
    /// </summary>
    /// <param name="doc">The PDF document whose form fields will be populated.</param>
    /// <param name="fdfPath">Path to the FDF file.</param>
    private static void FillFormFromFdf(Document doc, string fdfPath)
    {
        if (doc?.Form?.Fields == null)
            return;

        string fdfContent = File.ReadAllText(fdfPath);
        // Very simple regex to capture /T (field name) and /V (field value) pairs.
        // This works for basic FDF files; for complex structures a full parser would be required.
        var matches = Regex.Matches(fdfContent, @"/T\s*\(([^)]+)\)\s*/V\s*\(([^)]+)\)", RegexOptions.Singleline);
        foreach (Match match in matches)
        {
            string fieldName = match.Groups[1].Value;
            string fieldValue = match.Groups[2].Value;

            var field = doc.Form.Fields.FirstOrDefault(f =>
                string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase));

            if (field != null)
            {
                field.Value = fieldValue;
            }
        }
    }
}
