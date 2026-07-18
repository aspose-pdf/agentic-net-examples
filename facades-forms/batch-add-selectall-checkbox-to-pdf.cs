using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputFolder = "InputPdfs";
        // Directory where modified PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Ensure input directory exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDFs inside and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // nothing to process yet
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'." );
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Build output file path
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_with_checkbox.pdf");

            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Initialize FormEditor facade
                using (FormEditor formEditor = new FormEditor())
                {
                    // Bind the loaded document to the facade
                    formEditor.BindPdf(doc);

                    // Define checkbox rectangle (lower‑left x/y and upper‑right x/y)
                    // Adjust these values as needed for your layout
                    float llx = 50f;   // lower‑left X
                    float lly = 750f;  // lower‑left Y
                    float urx = 70f;   // upper‑right X
                    float ury = 770f;  // upper‑right Y

                    // Add a checkbox field named "SelectAll" on the first page
                    formEditor.AddField(FieldType.CheckBox, "SelectAll", 1, llx, lly, urx, ury);

                    // Save the modified PDF
                    formEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}
