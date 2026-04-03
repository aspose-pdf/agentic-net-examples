using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchFillForms
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where filled PDFs will be written
        const string outputFolder = "OutputPdfs";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{Path.GetFullPath(inputFolder)}' does not exist. No files to process.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the field names and the values to assign to every document
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToString("yyyy-MM-dd") }
        };

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Fill each predefined field if it exists in the current document
                foreach (var kvp in fieldValues)
                {
                    if (doc.Form.HasField(kvp.Key) && doc.Form[kvp.Key] is Field field)
                    {
                        field.Value = kvp.Value;
                    }
                }

                // Save the modified document (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
