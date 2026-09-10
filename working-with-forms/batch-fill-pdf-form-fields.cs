using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchFormFiller
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where filled PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Define the field values to apply to every PDF
        var fieldValues = new (string Name, string Value)[]
        {
            ("FirstName", "John"),
            ("LastName",  "Doe"),
            ("Date",      DateTime.Today.ToString("yyyy-MM-dd"))
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (using block ensures proper disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Optional: improve performance when many fields are updated
                    doc.Form.AutoRecalculate = false;

                    // Set each predefined field to its corresponding value
                    foreach (var (Name, Value) in fieldValues)
                    {
                        // If the field exists, assign the value using the Field API
                        if (doc.Form.HasField(Name))
                        {
                            // The indexer returns a Field; assign via the Value property
                            if (doc.Form[Name] is Field field)
                            {
                                field.Value = Value;
                            }
                        }
                    }

                    // Determine output file path (same name, different folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Save the modified PDF (Document.Save(string) writes PDF)
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
