using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationBatchProcessor
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Folder where exported XFDF files will be archived
        const string archiveFolder = @"C:\XfdfArchive";

        // Ensure output and archive directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(archiveFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPdfPath = Path.Combine(outputFolder, fileName + "_clean.pdf");
            string xfdfPath = Path.Combine(archiveFolder, fileName + ".xfdf");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize the annotation editor with the loaded document
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                    {
                        // Export all annotations to an XFDF file (use stream overload)
                        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                        {
                            editor.ExportAnnotationsToXfdf(xfdfStream);
                        }

                        // Delete all annotations from the document
                        editor.DeleteAnnotations();

                        // Save the cleaned PDF to the output folder
                        editor.Save(outputPdfPath);
                    }
                }

                Console.WriteLine($"Processed '{pdfPath}'. XFDF archived to '{xfdfPath}'. Clean PDF saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
