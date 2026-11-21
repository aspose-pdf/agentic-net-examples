using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string reportPath = "form_diff_report.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        var diffs = new List<string>();

        // Wrap both Document objects in using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Extract form fields from each PDF into dictionaries (field name -> value)
            var fields1 = GetFormFields(doc1);
            var fields2 = GetFormFields(doc2);

            // Create a set containing all unique field names from both PDFs
            var allNames = new HashSet<string>(fields1.Keys, StringComparer.OrdinalIgnoreCase);
            allNames.UnionWith(fields2.Keys);

            // Compare values for each field name
            foreach (var name in allNames)
            {
                fields1.TryGetValue(name, out string val1);
                fields2.TryGetValue(name, out string val2);

                if (val1 != val2)
                {
                    diffs.Add($"Field '{name}':");
                    diffs.Add($"  First PDF : {(val1 ?? "<null>")}");
                    diffs.Add($"  Second PDF: {(val2 ?? "<null>")}");
                }
            }
        }

        // Write the diff report to a text file
        if (diffs.Count == 0)
        {
            File.WriteAllText(reportPath, "No differences found in form fields.");
        }
        else
        {
            File.WriteAllLines(reportPath, diffs);
        }

        Console.WriteLine($"Form diff report written to '{reportPath}'.");
    }

    // Helper method to collect form fields from a Document into a dictionary
    static Dictionary<string, string> GetFormFields(Document doc)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Ensure the document actually contains a form
        if (doc.Form != null && doc.Form.Fields != null)
        {
            foreach (var field in doc.Form.Fields)
            {
                // Use FullName as the field identifier and convert the value to string
                string value = field.Value?.ToString();
                dict[field.FullName] = value;
            }
        }

        return dict;
    }
}