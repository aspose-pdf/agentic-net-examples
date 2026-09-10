using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Base directory of the application (works cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder containing PDFs to process
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        // Output folder for the filled PDFs
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate the input directory – if it does not exist, fall back to the current directory
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: '{inputFolder}'. Using current directory instead.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Define the field names and the values to set
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'." );
            return;
        }

        // Process each PDF in parallel to improve batch performance
        Parallel.ForEach(pdfFiles, inputPath =>
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_filled.pdf");

                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Iterate over the fields to fill
                    foreach (KeyValuePair<string, string> kvp in fieldValues)
                    {
                        // Retrieve the field by name; cast to the appropriate field type
                        TextBoxField? textBox = doc.Form[kvp.Key] as TextBoxField;
                        if (textBox != null)
                        {
                            // Set the field value
                            textBox.Value = kvp.Value;
                        }
                        // Add handling for other field types if needed (e.g., CheckBoxField, RadioButtonField)
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} -> {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}
