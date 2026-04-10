using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "ProcessedPdfs";
        // Folder where XFDF files are initially exported
        const string xfdfExportFolder = "XfdfExports";
        // Folder where exported XFDF files are archived
        const string xfdfArchiveFolder = "XfdfArchive";

        // Ensure all required directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(xfdfExportFolder);
        Directory.CreateDirectory(xfdfArchiveFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(xfdfExportFolder, fileNameWithoutExt + ".xfdf");
            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Use PdfAnnotationEditor to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(pdfPath);

                // Export all annotations to an XFDF file (use stream overload)
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                }

                // Delete all annotations from the document
                editor.DeleteAnnotations();

                // Save the modified PDF to the output folder
                editor.Save(outputPdfPath);

                // Close the editor (optional, Dispose will be called by using)
                editor.Close();
            }

            // Archive the exported XFDF file
            string archivedXfdfPath = Path.Combine(xfdfArchiveFolder, Path.GetFileName(xfdfPath));
            // If a file with the same name already exists in the archive, overwrite it
            if (File.Exists(archivedXfdfPath))
            {
                File.Delete(archivedXfdfPath);
            }
            File.Move(xfdfPath, archivedXfdfPath);
        }

        Console.WriteLine("Batch processing completed.");
    }
}
