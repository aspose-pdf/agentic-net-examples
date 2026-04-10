using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfPageEditor
using Aspose.Pdf;                 // Document, PageSize class (fully qualified when needed)

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine the required page size for this document
                PageSize targetSize = GetDesiredPageSize(sourcePath);

                // Build the output file path (overwrite same name in output folder)
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(outputFolder, fileName);

                // Use PdfPageEditor (Facade) to change the page size
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(sourcePath);
                    // Set the new page size for all pages
                    editor.PageSize = targetSize;
                    // Apply the changes to the document
                    editor.ApplyChanges();
                    // Save the modified PDF
                    editor.Save(destinationPath);
                }

                Console.WriteLine($"Processed '{fileName}' with page size {targetSize.Width}x{targetSize.Height}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }

    // Example logic to decide page size per document.
    // Replace with real business rules as needed.
    static PageSize GetDesiredPageSize(string pdfPath)
    {
        string name = Path.GetFileNameWithoutExtension(pdfPath).ToLowerInvariant();

        // Aspose.Pdf.PageSize does not expose Letter/Legal in recent versions.
        // Use explicit dimensions (points) for those sizes.
        // 1 point = 1/72 inch.
        // Letter  = 8.5" x 11"   => 612 x 792 points
        // Legal   = 8.5" x 14"   => 612 x 1008 points
        // A5      = 148 x 210 mm => 420 x 595 points (approx)
        // A4      = 210 x 297 mm => 595 x 842 points (approx)

        if (name.Contains("letter"))
            return new PageSize(612, 792); // Letter
        if (name.Contains("legal"))
            return new PageSize(612, 1008); // Legal
        if (name.Contains("a5"))
            return PageSize.A5;

        // Default page size (A4)
        return PageSize.A4;
    }
}
