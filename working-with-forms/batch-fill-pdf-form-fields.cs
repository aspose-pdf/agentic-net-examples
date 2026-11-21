using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field API

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Define the values to assign to fields (same for every PDF)
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToString("yyyy-MM-dd") }
        };

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath  = Path.Combine(outputFolder, fileName);

            try
            {
                // Load PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form)
                    {
                        // If the field name matches one of our predefined keys, set its value
                        if (fieldValues.TryGetValue(field.FullName, out string value))
                        {
                            field.Value = value;
                        }
                    }

                    // Save the modified PDF (PDF format by default)
                    doc.Save(outPath);
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