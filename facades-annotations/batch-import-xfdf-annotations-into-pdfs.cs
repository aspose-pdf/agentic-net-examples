using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchXfdfImporter
{
    static void Main()
    {
        // Folder containing PDFs and matching XFDF files
        const string inputFolder = "InputFiles";
        // Folder where PDFs with imported annotations will be saved
        const string outputFolder = "OutputFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, baseName + ".xfdf");

            // Skip if there is no matching XFDF file
            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No XFDF found for '{baseName}.pdf'; skipping.");
                continue;
            }

            string outputPdfPath = Path.Combine(outputFolder, baseName + ".pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize the annotation editor facade
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        // Bind the PDF to the editor
                        editor.BindPdf(doc);
                        // Import all annotations from the XFDF file
                        editor.ImportAnnotationsFromXfdf(xfdfPath);
                        // Save the modified PDF
                        editor.Save(outputPdfPath);
                    }
                }

                Console.WriteLine($"Imported XFDF into '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}