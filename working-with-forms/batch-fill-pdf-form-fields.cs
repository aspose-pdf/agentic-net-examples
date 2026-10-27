using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where the processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the fields to fill and the values to assign (same for every PDF)
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToString("yyyy-MM-dd") },
            { "Company",   "Acme Corp" }
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Iterate over the predefined field/value pairs
                    foreach (var kvp in fieldValues)
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                        Field? field = doc.Form[kvp.Key] as Field;
                        if (field != null)
                        {
                            // Set the field's value
                            field.Value = kvp.Value;
                        }
                    }

                    // Build the output file path (same file name, different folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
