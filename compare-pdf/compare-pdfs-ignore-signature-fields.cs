using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms; // needed for form field types

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // 1. Compare page counts
            if (doc1.Pages.Count != doc2.Pages.Count)
            {
                Console.WriteLine("Documents differ: page count mismatch.");
                return;
            }

            // 2. Gather signature field names from each document (to be ignored)
            HashSet<string> sigFields1 = GetSignatureFieldNames(doc1);
            HashSet<string> sigFields2 = GetSignatureFieldNames(doc2);

            // 3. Compare non‑signature form fields
            if (!CompareFormFields(doc1, doc2, sigFields1, sigFields2))
            {
                Console.WriteLine("Documents differ: non‑signature form fields mismatch.");
                return;
            }

            // 4. Compare visible text on each page (signature fields usually have no visible text)
            for (int i = 1; i <= doc1.Pages.Count; i++)
            {
                string text1 = ExtractPageText(doc1.Pages[i]);
                string text2 = ExtractPageText(doc2.Pages[i]);

                if (!string.Equals(text1, text2, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Documents differ: text mismatch on page {i}.");
                    return;
                }
            }

            Console.WriteLine("Documents are equivalent (signature fields ignored).");
        }
    }

    // Returns a set of field names whose concrete type is SignatureField
    private static HashSet<string> GetSignatureFieldNames(Document doc)
    {
        var result = new HashSet<string>(StringComparer.Ordinal);
        if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Any())
        {
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField && !string.IsNullOrEmpty(field.FullName))
                {
                    result.Add(field.FullName);
                }
            }
        }
        return result;
    }

    // Compare all form fields except those listed as signature fields
    private static bool CompareFormFields(Document doc1, Document doc2,
                                          HashSet<string> sigFields1,
                                          HashSet<string> sigFields2)
    {
        // Build dictionaries of non‑signature fields (name -> value)
        var dict1 = BuildFieldDictionary(doc1, sigFields1);
        var dict2 = BuildFieldDictionary(doc2, sigFields2);

        if (dict1.Count != dict2.Count)
            return false;

        foreach (var kvp in dict1)
        {
            if (!dict2.TryGetValue(kvp.Key, out string value2))
                return false;

            if (!string.Equals(kvp.Value, value2, StringComparison.Ordinal))
                return false;
        }
        return true;
    }

    // Helper to create a name‑value map of fields excluding signature fields
    private static Dictionary<string, string> BuildFieldDictionary(Document doc, HashSet<string> sigFields)
    {
        var dict = new Dictionary<string, string>(StringComparer.Ordinal);
        if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Any())
        {
            foreach (var field in doc.Form.Fields)
            {
                // Skip any signature field (or any field whose name is in the signature set)
                if (field is SignatureField || sigFields.Contains(field.FullName))
                    continue;

                if (string.IsNullOrEmpty(field.FullName))
                    continue;

                // For most field types the Value property holds the user‑entered data.
                // Not all concrete field classes expose Value, so we use reflection as a fallback.
                string value = string.Empty;
                try
                {
                    var valProp = field.GetType().GetProperty("Value");
                    if (valProp != null)
                    {
                        var val = valProp.GetValue(field);
                        value = val?.ToString() ?? string.Empty;
                    }
                }
                catch { /* ignore any reflection issues */ }

                dict[field.FullName] = value;
            }
        }
        return dict;
    }

    // Extracts all visible text from a page using TextAbsorber
    private static string ExtractPageText(Page page)
    {
        var absorber = new TextAbsorber();
        page.Accept(absorber);
        return absorber.Text ?? string.Empty;
    }
}
