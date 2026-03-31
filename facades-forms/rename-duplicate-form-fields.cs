using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal one with duplicate fields if necessary
        if (!File.Exists(inputPath))
        {
            CreateSamplePdfWithDuplicateFields(inputPath);
        }

        // Use the Facades.Form explicitly to avoid ambiguity with Aspose.Pdf.Forms.Form
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(inputPath))
        {
            // Capture the original list of field names
            string[] fieldNames = form.FieldNames;
            List<string> originalFieldNames = new List<string>(fieldNames);

            // Dictionary to count occurrences of each field name
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.Ordinal);

            foreach (string fieldName in originalFieldNames)
            {
                if (nameCounts.ContainsKey(fieldName))
                {
                    // Increment the counter for this duplicate
                    int duplicateIndex = ++nameCounts[fieldName];
                    // Build the new unique name
                    string newFieldName = $"{fieldName}_{duplicateIndex}";
                    // Rename the field
                    form.RenameField(fieldName, newFieldName);
                }
                else
                {
                    // First occurrence of this name
                    nameCounts[fieldName] = 0;
                }
            }

            // Save the modified PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Duplicate fields have been renamed and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a simple PDF containing a form with duplicate field names for demonstration purposes.
    /// </summary>
    private static void CreateSamplePdfWithDuplicateFields(string path)
    {
        // Create a new PDF document
        var doc = new Document();
        var page = doc.Pages.Add();

        // Add a text box field named "SampleField"
        var txt1 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 720))
        {
            PartialName = "SampleField",
            Value = "First"
        };
        doc.Form.Add(txt1);

        // Add another text box field with the same name to simulate a duplicate
        var txt2 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 200, 670))
        {
            PartialName = "SampleField",
            Value = "Second"
        };
        doc.Form.Add(txt2);

        // Save the document so the rest of the program can work with it
        doc.Save(path);
    }
}
