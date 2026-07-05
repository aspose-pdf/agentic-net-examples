using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFieldFiller
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
        string outputDirectory = "FilledOutputs";
        Directory.CreateDirectory(outputDirectory);

        // Example field values to apply to every document
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                // Build output file path
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_filled.pdf");

                // Load, fill fields, and save the document
                using (Document doc = new Document(inputPath))
                {
                    foreach (var kvp in fieldValues)
                    {
                        // Verify the field exists before assigning a value
                        if (doc.Form.HasField(kvp.Key))
                        {
                            // The Form indexer returns a WidgetAnnotation; cast it to Field
                            Field? formField = doc.Form[kvp.Key] as Field;
                            if (formField != null)
                            {
                                formField.Value = kvp.Value;
                            }
                        }
                    }

                    // Save the modified PDF (PDF format is the default)
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
