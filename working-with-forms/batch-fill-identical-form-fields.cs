using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfFolder\Input";
        // Folder where the filled PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Output";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Define the field names and the values to set (identical for all PDFs)
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Agreement", "Yes" }
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Access the form object of the document
                    Form form = doc.Form;

                    // Iterate over the predefined field/value pairs
                    foreach (KeyValuePair<string, string> kvp in fieldValues)
                    {
                        // Check if the field exists in the current document
                        if (form.HasField(kvp.Key))
                        {
                            // The Form indexer returns a WidgetAnnotation; cast it to Field
                            Field field = form[kvp.Key] as Field;
                            if (field != null)
                            {
                                field.Value = kvp.Value;
                            }
                        }
                    }

                    // Save the modified document (overwrites if the same path is used)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
