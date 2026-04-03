using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Needed for Field type

class Program
{
    static void Main()
    {
        // Input PDF files (could be gathered from a directory)
        const string inputFolder = "InputPdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return; // Exit gracefully instead of throwing DirectoryNotFoundException
        }

        string[] inputFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (inputFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // Output directory for the filled PDFs
        const string outputDir = "OutputPdfs";
        Directory.CreateDirectory(outputDir);

        // Example field values to apply to each PDF
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToShortDateString() }
        };

        // Process each PDF in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Fill form fields
                    foreach (var kvp in fieldValues)
                    {
                        // Check if the field exists before setting its value
                        if (doc.Form.HasField(kvp.Key))
                        {
                            // Retrieve the field as a Form.Field (derived from WidgetAnnotation)
                            Field field = doc.Form[kvp.Key] as Field;
                            if (field != null)
                            {
                                field.Value = kvp.Value; // Use Field.Value, not WidgetAnnotation.Value
                            }
                        }
                    }

                    // Determine output file path (preserve original file name)
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

                    // Save the modified PDF (PDF format, no SaveOptions needed)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}
