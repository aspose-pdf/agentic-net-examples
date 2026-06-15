using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchFormFiller
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new[]
        {
            "Invoice1.pdf",
            "Invoice2.pdf",
            "Invoice3.pdf"
            // add more file paths as needed
        };

        // Directory where filled PDFs will be saved
        string outputDirectory = "FilledOutputs";
        Directory.CreateDirectory(outputDirectory);

        // Example field values to apply to each document
        var fieldValues = new Dictionary<string, string>
        {
            { "CustomerName", "John Doe" },
            { "Date", DateTime.Today.ToString("yyyy-MM-dd") },
            { "Amount", "1234.56" }
        };

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                // Ensure the source file exists
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                // Determine output file path
                string outputPath = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(inputPath) + "_filled.pdf");

                // Load, fill, and save the document
                using (Document doc = new Document(inputPath))
                {
                    // Disable automatic recalculation for better performance
                    doc.Form.AutoRecalculate = false;

                    // Fill each specified field if it exists in the document
                    foreach (var kvp in fieldValues)
                    {
                        string fieldName = kvp.Key;
                        string fieldValue = kvp.Value;

                        if (doc.Form.HasField(fieldName))
                        {
                            // The indexer may return a WidgetAnnotation in some versions;
                            // cast to Field to access the Value property safely.
                            if (doc.Form[fieldName] is Field field)
                            {
                                field.Value = fieldValue;
                            }
                        }
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}
