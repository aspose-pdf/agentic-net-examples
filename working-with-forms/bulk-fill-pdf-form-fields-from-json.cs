using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Bulk data to fill – some keys may not exist in the PDF form
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() },
            { "MissingField", "This field does not exist and will be skipped" }
        };

        try
        {
            // Ensure deterministic disposal of the Document
            using (Document pdf = new Document(templatePath))
            {
                // Verify that the PDF actually contains a form
                if (pdf.Form == null)
                {
                    Console.Error.WriteLine("The PDF does not contain any form fields.");
                }
                else
                {
                    // Iterate over each entry and attempt to set the value
                    foreach (var kvp in fieldValues)
                    {
                        try
                        {
                            // The Form indexer returns a WidgetAnnotation; cast safely to Field
                            Field? field = pdf.Form[kvp.Key] as Field;

                            // If the field is missing or not a form field, skip it
                            if (field == null)
                            {
                                Console.WriteLine($"Field '{kvp.Key}' not found – skipping.");
                                continue;
                            }

                            // If the field is a TextBoxField, set its value; otherwise skip
                            if (field is TextBoxField textBox)
                            {
                                textBox.Value = kvp.Value;
                            }
                            else
                            {
                                Console.WriteLine($"Field '{kvp.Key}' is present but not a TextBoxField – skipping.");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the error for this specific field and continue
                            Console.WriteLine($"Error processing field '{kvp.Key}': {ex.Message}");
                        }
                    }
                }

                // Save the filled PDF
                pdf.Save(outputPath);
            }

            Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., file I/O)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
