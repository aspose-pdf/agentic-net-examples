using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        const string inputDirectory  = "InputPdfs";
        // Directory where the modified PDFs will be saved
        const string outputDirectory = "OutputPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return;
        }

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Determine the output file path (same name, different folder)
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

            // Open the source PDF document
            using (Document doc = new Document(inputPath))
            {
                // Initialize the FormEditor facade for the opened document
                using (FormEditor formEditor = new FormEditor(doc))
                {
                    // Add a checkbox field named "SelectAll" on the first page
                    // Coordinates: lower‑left (llx, lly) = (50, 750), upper‑right (urx, ury) = (70, 770)
                    // Adjust these values as needed for your layout
                    bool added = formEditor.AddField(
                        FieldType.CheckBox,   // Field type
                        "SelectAll",          // Field name
                        1,                    // Page number (1‑based)
                        50f, 750f, 70f, 770f // llx, lly, urx, ury
                    );

                    if (!added)
                    {
                        Console.Error.WriteLine($"Failed to add field to '{inputPath}'.");
                    }

                    // Save the modified PDF to the output location
                    formEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}
