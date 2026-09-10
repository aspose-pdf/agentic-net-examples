using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Directories for input and output PDFs (created if missing)
        string inputDirectory  = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputPdfs");
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Verify that there is at least one PDF to process
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            try
            {
                // Bind the PDF to a FormEditor instance (non‑obsolete overload)
                using (FormEditor editor = new FormEditor())
                {
                    editor.BindPdf(inputPath);

                    // Configure visual attributes for the fields
                    editor.Facade = new FormFieldFacade();
                    editor.Facade.Alignment = FormFieldFacade.AlignCenter;

                    // Apply the visual attributes to all checkbox fields in the document
                    editor.DecorateField(FieldType.CheckBox);

                    // Save the modified PDF to the output location
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Decorated: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
