using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs and matching XFDF files
        const string inputFolder = "InputPdfs";

        // Folder where PDFs with imported annotations will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, baseName + ".xfdf");

            // Skip PDFs that do not have a matching XFDF file
            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No matching XFDF for '{baseName}.pdf' – skipping.");
                continue;
            }

            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Use PdfAnnotationEditor (Facades API) to import annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF document
                    editor.BindPdf(pdfPath);

                    // Import all annotations from the corresponding XFDF file
                    editor.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the updated PDF to the output folder
                    editor.Save(outputPdfPath);
                }

                Console.WriteLine($"Imported annotations into '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }
    }
}