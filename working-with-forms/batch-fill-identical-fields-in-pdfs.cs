using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where filled PDFs will be written
        const string outputFolder = "OutputPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Define the field names and the values to assign to every PDF
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToString("yyyy-MM-dd") }
        };

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Fill each predefined field with its corresponding value
                foreach (var kvp in fieldValues)
                {
                    string fieldName  = kvp.Key;
                    string fieldValue = kvp.Value;

                    // Verify that the field exists before setting its value
                    if (doc.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a Field; use pattern matching to avoid null warnings
                        if (doc.Form[fieldName] is Field field)
                        {
                            field.Value = fieldValue;
                        }
                    }
                }

                // Save the modified document (overwrites or creates a new file)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
