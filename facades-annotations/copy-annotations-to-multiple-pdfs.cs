using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the source annotations (template)
        const string templatePdfPath = "template.pdf";

        // Folder that contains the target PDFs to which annotations will be copied
        const string targetFolder = "Targets";

        // Folder where the annotated PDFs will be saved
        const string outputFolder = "Output";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Ensure the target folder exists – if it does not, create it (will be empty) and exit gracefully
        if (!Directory.Exists(targetFolder))
        {
            Console.WriteLine($"Target folder '{targetFolder}' does not exist. Creating it now.");
            Directory.CreateDirectory(targetFolder);
            Console.WriteLine("No PDF files to process. Place PDFs in the Targets folder and rerun the program.");
            return;
        }

        // Collect all PDF files in the target folder
        string[] targetPdfPaths = Directory.GetFiles(targetFolder, "*.pdf");
        if (targetPdfPaths.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{targetFolder}'." );
            return;
        }

        // Export annotations from the template PDF to an in‑memory XFDF stream
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
            {
                exporter.BindPdf(templatePdfPath);
                exporter.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Reset stream position so it can be read for each import
            xfdfStream.Position = 0;

            // Iterate over each target PDF, import the annotations, and save the result
            foreach (string targetPath in targetPdfPaths)
            {
                // Determine output file name (e.g., "document.pdf" -> "document_annotated.pdf")
                string fileName = Path.GetFileNameWithoutExtension(targetPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_annotated.pdf");

                // Import annotations from the XFDF stream into the target PDF
                using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
                {
                    importer.BindPdf(targetPath);
                    // Reset stream position before each import
                    xfdfStream.Position = 0;
                    importer.ImportAnnotationsFromXfdf(xfdfStream);
                    importer.Save(outputPath);
                }

                Console.WriteLine($"Annotations copied to: {outputPath}");
            }
        }
    }
}
