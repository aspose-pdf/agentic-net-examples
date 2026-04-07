using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Forms;               // For form field access
using Aspose.Pdf.Annotations;         // For widget‑related types (if needed)

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new string[]
        {
            "Form1.pdf",
            "Form2.pdf",
            "Form3.pdf"
        };

        // Directory where filled PDFs will be saved
        string outputDirectory = "FilledForms";
        Directory.CreateDirectory(outputDirectory);

        // Example field values to apply to each document
        // Key = field name in the PDF, Value = value to set
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToShortDateString() }
        };

        // Parallel processing options (optional)
        ParallelOptions parallelOptions = new ParallelOptions {
            // Adjust the degree of parallelism as needed; -1 = default (environment processor count)
            MaxDegreeOfParallelism = -1
        };

        // Process each PDF concurrently
        Parallel.ForEach(inputFiles, parallelOptions, inputPath =>
        {
            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Determine output file path
            string outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputPath) + "_filled.pdf");

            // Load, fill, and save the document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine($"No form fields found in: {inputPath}");
                }
                else
                {
                    // Iterate over the supplied field values and set them if the field exists
                    foreach (var kvp in fieldValues)
                    {
                        string fieldName = kvp.Key;
                        string fieldValue = kvp.Value;

                        // Check if the field exists in the current document
                        if (doc.Form.HasField(fieldName))
                        {
                            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                            Field? field = doc.Form[fieldName] as Field;
                            if (field == null)
                            {
                                Console.WriteLine($"Field '{fieldName}' exists but could not be cast to a form Field in {inputPath}");
                                continue;
                            }

                            switch (field)
                            {
                                case TextBoxField txtField:
                                    txtField.Value = fieldValue;
                                    break;

                                case CheckboxField chkField:
                                    // Treat "true" (case‑insensitive) as checked, everything else as unchecked
                                    chkField.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                                    break;

                                // Add handling for other field types as required (e.g., RadioButtonField, ListBoxField, etc.)
                                default:
                                    // Most field types expose a generic Value property.
                                    field.Value = fieldValue;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Field '{fieldName}' not found in {inputPath}");
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {outputPath}");
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}
