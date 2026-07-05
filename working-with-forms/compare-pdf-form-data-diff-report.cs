using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class FormDataComparer
{
    static void Main()
    {
        // Input PDF files containing form fields
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";

        // Output PDF that will contain the diff report
        const string reportPath = "form_diff_report.pdf";

        // Validate input files
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two source PDFs
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Extract form fields from both documents
            var fields1 = GetFormFields(doc1);
            var fields2 = GetFormFields(doc2);

            // Determine mismatched fields
            var mismatches = FindMismatchedFields(fields1, fields2);

            // Generate a PDF report summarizing the differences
            using (Document report = new Document())
            {
                // Add a blank page to host the table
                Page page = report.Pages.Add();

                // Create a table with three columns: Field Name, Value in PDF1, Value in PDF2
                Table table = new Table
                {
                    ColumnWidths = "150 200 200"
                    // Note: Table border handling is omitted because Border class requires an Annotation parent.
                };

                // Header row
                Row header = table.Rows.Add();
                AddCell(header, "Field Name", true);
                AddCell(header, "Value (PDF 1)", true);
                AddCell(header, "Value (PDF 2)", true);

                // Add a row for each mismatched field
                foreach (var mismatch in mismatches)
                {
                    Row row = table.Rows.Add();
                    AddCell(row, mismatch.FieldName, false);
                    AddCell(row, mismatch.Value1 ?? "(null)", false);
                    AddCell(row, mismatch.Value2 ?? "(null)", false);
                }

                // If there are no mismatches, indicate that the forms are identical
                if (mismatches.Count == 0)
                {
                    Row row = table.Rows.Add();
                    Cell cell = row.Cells.Add();
                    cell.ColSpan = 3;
                    cell.Paragraphs.Add(new TextFragment("No differences found – all form fields match."));
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the diff report
                report.Save(reportPath);
            }

            Console.WriteLine($"Form data diff report saved to '{reportPath}'.");
        }
    }

    // Helper method to extract form field name/value pairs from a document
    private static Dictionary<string, string> GetFormFields(Document doc)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (doc.Form != null && doc.Form.Fields != null)
        {
            foreach (var field in doc.Form.Fields)
            {
                // Some field types expose the value via the 'Value' property
                string value = field.Value?.ToString() ?? string.Empty;
                dict[field.FullName] = value;
            }
        }
        return dict;
    }

    // Helper method to compare two dictionaries and return mismatched entries
    private static List<Mismatch> FindMismatchedFields(
        Dictionary<string, string> dict1,
        Dictionary<string, string> dict2)
    {
        var mismatches = new List<Mismatch>();

        // Union of all field names from both PDFs
        var allKeys = new HashSet<string>(dict1.Keys, StringComparer.OrdinalIgnoreCase);
        allKeys.UnionWith(dict2.Keys);

        foreach (var key in allKeys)
        {
            dict1.TryGetValue(key, out string val1);
            dict2.TryGetValue(key, out string val2);

            // Treat null/empty as equivalent for missing fields
            if (!string.Equals(val1 ?? string.Empty, val2 ?? string.Empty, StringComparison.Ordinal))
            {
                mismatches.Add(new Mismatch
                {
                    FieldName = key,
                    Value1 = val1,
                    Value2 = val2
                });
            }
        }

        return mismatches;
    }

    // Helper method to add a cell with optional header styling
    private static void AddCell(Row row, string text, bool isHeader)
    {
        Cell cell = row.Cells.Add();
        TextFragment fragment = new TextFragment(text);
        if (isHeader)
        {
            fragment.TextState.FontSize = 12;
            fragment.TextState.FontStyle = FontStyles.Bold;
            fragment.TextState.ForegroundColor = Color.White;
            cell.BackgroundColor = Color.Gray;
        }
        else
        {
            fragment.TextState.FontSize = 10;
            fragment.TextState.ForegroundColor = Color.Black;
        }
        cell.Paragraphs.Add(fragment);
    }

    // Simple DTO to hold mismatch information (nullable strings to avoid null‑reference warnings)
    private class Mismatch
    {
        public string FieldName { get; set; } = string.Empty;
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
    }
}
