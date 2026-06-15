using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // Needed for accessing form fields

class Program
{
    // Checks that every required form field (WidgetAnnotation) has a non‑empty value.
    static bool AllRequiredFieldsFilled(Document doc)
    {
        // Iterate through all pages (1‑based indexing)
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];

            // Annotations collection is also 1‑based
            for (int j = 1; j <= page.Annotations.Count; j++)
            {
                Annotation ann = page.Annotations[j];

                // Only WidgetAnnotation (form fields) have the Required property
                if (ann is WidgetAnnotation widget && widget.Required)
                {
                    // Retrieve the corresponding form field using the widget's name.
                    // Document.Form indexer returns a WidgetAnnotation, so we must cast to Field.
                    Field field = doc.Form[widget.Name] as Field;
                    if (field == null)
                    {
                        Console.Error.WriteLine(
                            $"Required widget on page {i}, annotation {j} has no matching form field (Name: {widget.Name}).");
                        return false;
                    }

                    // The field value can be null, empty string or whitespace
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.Error.WriteLine(
                            $"Required field missing value on page {i}, annotation {j} (Name: {widget.Name}).");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Validate required fields before saving
                if (!AllRequiredFieldsFilled(doc))
                {
                    Console.Error.WriteLine("Document contains unfilled required fields. Save aborted.");
                    return;
                }

                // Optional: run built‑in PDF validation (repairs if needed)
                doc.Check(doRepair: true);

                // Save the document only after successful validation
                doc.Save(outputPath);
                Console.WriteLine($"Document saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
