using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchXfdfImporter
{
    static void Main()
    {
        // Folder containing PDFs and XFDF files
        const string inputFolder = @"C:\Input";
        // Folder where PDFs with imported annotations will be saved
        const string outputFolder = @"C:\Output";

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
                Console.WriteLine($"No XFDF found for '{baseName}.pdf' – skipping.");
                continue;
            }

            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize the annotation editor facade
                    PdfAnnotationEditor editor = new PdfAnnotationEditor();
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);
                    // Import all annotations from the matching XFDF file
                    editor.ImportAnnotationsFromXfdf(xfdfPath);
                    // Save the updated PDF (save rule: direct Save for PDF)
                    editor.Save(outputPdfPath);
                }

                Console.WriteLine($"Imported annotations from '{baseName}.xfdf' into '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}.pdf': {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}