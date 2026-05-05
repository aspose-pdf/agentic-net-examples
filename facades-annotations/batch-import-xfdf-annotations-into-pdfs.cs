using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchXfdfImporter
{
    static void Main()
    {
        // Resolve input and output folders relative to the executable location.
        // This works on both Windows and Unix‑based systems.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.GetFullPath(Path.Combine(baseDir, "Input"));
        string outputFolder = Path.GetFullPath(Path.Combine(baseDir, "Output"));

        // Ensure both directories exist (creates them if they are missing).
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, fileNameWithoutExt + ".xfdf");

            // Skip if there is no matching XFDF file
            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No XFDF found for '{pdfPath}'. Skipping.");
                continue;
            }

            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Use PdfAnnotationEditor (facade) to bind the PDF, import annotations, and save
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);                         // Load the PDF
                    editor.ImportAnnotationsFromXfdf(xfdfPath);      // Import matching XFDF annotations
                    editor.Save(outputPdfPath);                      // Save the updated PDF
                }

                Console.WriteLine($"Imported XFDF into '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}
