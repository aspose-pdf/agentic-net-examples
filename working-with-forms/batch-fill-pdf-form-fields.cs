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

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the field names and the values to assign to every PDF
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() },
            { "Approved", "Yes" }
        };

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF (using ensures deterministic disposal)
                using (Document doc = new Document(sourcePath))
                {
                    // Disable automatic recalculation for better performance when many fields are set
                    doc.Form.AutoRecalculate = false;

                    // Set each predefined field value if the field exists in the document
                    foreach (var kvp in fieldValues)
                    {
                        if (doc.Form.HasField(kvp.Key))
                        {
                            // Retrieve the field as a Field object (not WidgetAnnotation)
                            Field field = doc.Form[kvp.Key] as Field;
                            if (field != null)
                            {
                                field.Value = kvp.Value; // <-- correct way to assign a value
                            }
                            else
                            {
                                Console.WriteLine($"Field '{kvp.Key}' exists but is not a standard form field in {fileName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Field '{kvp.Key}' not found in {fileName}");
                        }
                    }

                    // Save the modified PDF (overwrites the file in the output folder)
                    doc.Save(destinationPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
