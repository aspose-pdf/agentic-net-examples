using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string pdfFolder = "pdfs";
        // Folder containing XFDF files (same base name as PDFs)
        const string xfdfFolder = "xfdfs";
        // Folder where PDFs with imported annotations will be saved
        const string outputFolder = "output";

        // Ensure the required folders exist (create if missing for output, warn for source folders)
        if (!Directory.Exists(pdfFolder))
        {
            Console.WriteLine($"Source PDF folder '{pdfFolder}' does not exist. Nothing to process.");
            return;
        }
        if (!Directory.Exists(xfdfFolder))
        {
            Console.WriteLine($"XFDF folder '{xfdfFolder}' does not exist. Skipping annotation import.");
            // We can still continue – PDFs will be copied unchanged if desired, but per original logic we exit.
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Iterate over all PDF files in the source folder
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(xfdfFolder, baseName + ".xfdf");

            // Skip if there is no matching XFDF file
            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No XFDF found for '{baseName}'. Skipping.");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Initialize the annotation editor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the PDF document to the editor
                    editor.BindPdf(doc);
                    // Import all annotations from the matching XFDF file
                    editor.ImportAnnotationsFromXfdf(xfdfPath);
                    // Save the updated PDF (lifecycle rule: use Save)
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed '{baseName}'. Saved to '{outputPath}'.");
        }
    }
}
