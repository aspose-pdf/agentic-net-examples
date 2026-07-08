using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType
using Aspose.Pdf;          // Document (if needed for other operations)

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (e.g., MyDoc_SelectAll.pdf)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_SelectAll.pdf");

            try
            {
                // Use FormEditor (a Facades class) to edit the form fields
                using (FormEditor formEditor = new FormEditor())
                {
                    // Bind the source PDF to the editor
                    formEditor.BindPdf(inputPath);

                    // Add a checkbox field named "SelectAll" on the first page.
                    // Coordinates are in points (1 point = 1/72 inch).
                    // Adjust the rectangle as needed for your layout.
                    // llx, lly = lower‑left corner; urx, ury = upper‑right corner.
                    bool fieldAdded = formEditor.AddField(
                        FieldType.CheckBox,   // type of field to add
                        "SelectAll",          // field name
                        1,                    // page number (1‑based indexing)
                        100f, 700f,           // lower‑left X, Y
                        120f, 720f);          // upper‑right X, Y

                    if (!fieldAdded)
                    {
                        Console.Error.WriteLine($"Failed to add checkbox to {inputPath}");
                    }

                    // Save the modified PDF to the output path
                    formEditor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}