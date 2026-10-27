using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source_form.pdf";
        const string targetPdf = "target_template.pdf";
        const string outputPdf = "merged_filled.pdf";

        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Extract form data from the source PDF
        // ---------------------------------------------------------------------
        var extractedData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (Document srcDoc = new Document(sourcePdf))
        {
            var form = srcDoc.Form;
            // FormFieldCollection exposes a Count() extension – use it via LINQ.
            if (form != null && form.Fields.Count() > 0)
            {
                foreach (var field in form.Fields)
                {
                    // Use the FormField API (PartialName / Value)
                    string name = field.PartialName;
                    string value = field.Value?.ToString() ?? string.Empty;
                    extractedData[name] = value;
                }
            }
        }

        // ---------------------------------------------------------------------
        // 2. Transform data to a new schema (example: rename keys and uppercase values)
        // ---------------------------------------------------------------------
        var transformedData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var kvp in extractedData)
        {
            string newName = "new_" + kvp.Key;
            string newValue = kvp.Value.ToUpperInvariant();
            transformedData[newName] = newValue;
        }

        // ---------------------------------------------------------------------
        // 3. Load the target PDF and import transformed data as new form fields
        // ---------------------------------------------------------------------
        using (Document tgtDoc = new Document(targetPdf))
        {
            // Use the first page as the container for new fields (adjust as needed)
            Page page = tgtDoc.Pages[1]; // 1‑based indexing

            double startY = 750; // initial vertical position
            foreach (var kvp in transformedData)
            {
                // Define a rectangle for the field (position/size can be adjusted)
                double llx = 50;
                double lly = startY;
                double urx = 300;
                double ury = lly + 15;

                // Create a TextBoxField and set its properties
                TextBoxField txtField = new TextBoxField(tgtDoc, new Rectangle(llx, lly, urx, ury))
                {
                    PartialName = kvp.Key,
                    Value = kvp.Value
                };

                // Set the font size via the DefaultAppearance property (FontSize is not a direct member)
                txtField.DefaultAppearance.FontSize = 12;

                // Add the field to the document's form collection
                tgtDoc.Form.Add(txtField);

                // Move the next field down
                startY -= 20;
            }

            // Save the resulting PDF
            tgtDoc.Save(outputPdf);
        }

        Console.WriteLine($"Form data transferred and saved to '{outputPdf}'.");
    }
}
