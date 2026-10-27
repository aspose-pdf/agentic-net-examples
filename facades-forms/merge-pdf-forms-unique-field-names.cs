using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // needed for Form facade

// Alias to disambiguate the Form class from Aspose.Pdf.Facades
using FacadeForm = Aspose.Pdf.Facades.Form;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_renamed.pdf");

            try
            {
                // Load the PDF document using the main Aspose.Pdf API
                Document pdfDocument = new Document(inputPath);

                // Ensure the document contains a form
                if (pdfDocument.Form == null || pdfDocument.Form.Fields == null)
                {
                    Console.WriteLine($"No form fields found in '{Path.GetFileName(inputPath)}'. Skipping.");
                    continue;
                }

                // Retrieve all field names (full names are case‑insensitive in PDF spec)
                string[] fieldNames = pdfDocument.Form.Fields.Select(f => f.FullName).ToArray();

                // Dictionary to track how many times a name has been seen (case‑insensitive)
                var nameOccurrences = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Use the Form facade – it provides the RenameField method which the
                // Aspose.Pdf.Forms.Form class does not expose.
                using (FacadeForm formFacade = new FacadeForm(pdfDocument))
                {
                    foreach (string originalName in fieldNames)
                    {
                        // Determine current occurrence count
                        nameOccurrences.TryGetValue(originalName, out int count);

                        if (count == 0)
                        {
                            // First occurrence – keep the original name
                            nameOccurrences[originalName] = 1;
                        }
                        else
                        {
                            // Duplicate – create a new unique name by appending an index
                            string newName;
                            int suffix = count; // start with current count
                            do
                            {
                                newName = $"{originalName}_{suffix}";
                                suffix++;
                            }
                            // Ensure the generated name does not clash with any existing field
                            while (Array.Exists(fieldNames, n => n.Equals(newName, StringComparison.OrdinalIgnoreCase))
                                   || nameOccurrences.ContainsKey(newName));

                            // Rename the field using the facade
                            formFacade.RenameField(originalName, newName);

                            // Update occurrence counters
                            nameOccurrences[originalName] = count + 1;
                            nameOccurrences[newName] = 1;
                        }
                    }
                }

                // Save the modified PDF
                pdfDocument.Save(outputPath);
                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
