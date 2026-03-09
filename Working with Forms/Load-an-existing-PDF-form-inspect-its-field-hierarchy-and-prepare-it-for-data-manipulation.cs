using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "prepared_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF form inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Disable automatic recalculation while we manipulate fields for better performance
                doc.Form.AutoRecalculate = false;

                // Inspect and display the field hierarchy
                Console.WriteLine($"Total top‑level fields: {doc.Form.Count}");
                foreach (Field field in doc.Form.Fields)
                {
                    PrintFieldInfo(field, 0);
                }

                // Example: set a value for a specific field if it exists
                const string targetFieldName = "CustomerName";
                if (doc.Form.HasField(targetFieldName, true))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                    Field? targetField = doc.Form[targetFieldName] as Field;
                    if (targetField != null)
                    {
                        targetField.Value = "John Doe";
                        Console.WriteLine($"Set value of field '{targetFieldName}' to 'John Doe'.");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{targetFieldName}' exists but is not a form field.");
                    }
                }
                else
                {
                    Console.WriteLine($"Field '{targetFieldName}' not found in the document.");
                }

                // Re‑enable auto‑recalculation if further operations require it
                doc.Form.AutoRecalculate = true;

                // Save the prepared PDF
                doc.Save(outputPath);
                Console.WriteLine($"Prepared PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursive helper to print field information with indentation
    static void PrintFieldInfo(Field field, int indentLevel)
    {
        string indent = new string(' ', indentLevel * 2);
        Console.WriteLine($"{indent}Field: {field.FullName} (Partial: {field.PartialName})");

        // If the field is a group (contains child fields), iterate them recursively
        if (field.IsGroup && field.Count > 0)
        {
            foreach (WidgetAnnotation sub in field)
            {
                if (sub is Field subField)
                {
                    PrintFieldInfo(subField, indentLevel + 1);
                }
            }
        }
    }
}
