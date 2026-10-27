using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFieldFiller
{
    // Define the fields to fill and their values (same for all PDFs in this example)
    private static readonly Dictionary<string, string> FieldValues = new Dictionary<string, string>
    {
        { "Name", "John Doe" },
        { "Date", DateTime.Today.ToString("yyyy-MM-dd") },
        { "Company", "Acme Corp" }
    };

    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Output folder where filled PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Parallel options – limit degree of parallelism to avoid overwhelming the system
        ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, parallelOptions, inputPath =>
        {
            try
            {
                // Determine output file path (preserve original name)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Iterate over the predefined field/value pairs
                    foreach (var kvp in FieldValues)
                    {
                        // Retrieve the form field by name; the Form indexer returns a WidgetAnnotation, so cast to Field
                        Field field = doc.Form[kvp.Key] as Field;
                        if (field != null)
                        {
                            // Handle common field types – currently only TextBoxField, CheckboxField and RadioButtonOptionField are used
                            if (field is TextBoxField txtField)
                            {
                                txtField.Value = kvp.Value;
                            }
                            else if (field is CheckboxField chkField)
                            {
                                // Example: set check box based on a boolean string ("true"/"false")
                                chkField.Checked = string.Equals(kvp.Value, "true", StringComparison.OrdinalIgnoreCase);
                            }
                            else if (field is RadioButtonOptionField radioField)
                            {
                                // Example: set selected option by value
                                radioField.Value = kvp.Value;
                            }
                            else
                            {
                                // Fallback: attempt to set generic Value property via dynamic cast
                                try
                                {
                                    dynamic dyn = field;
                                    dyn.Value = kvp.Value;
                                }
                                catch { /* ignore unsupported field types */ }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Field '{kvp.Key}' not found in '{Path.GetFileName(inputPath)}'.");
                        }
                    }

                    // Save the modified PDF; Document.Save(string) writes PDF regardless of extension
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch field filling completed.");
    }
}
