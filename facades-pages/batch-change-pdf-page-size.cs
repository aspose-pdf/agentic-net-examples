using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchPageSizeProcessor
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Define required page size per document (file name without extension as key)
        // Add entries as needed; default will be A4 if not specified.
        var pageSizeMap = new Dictionary<string, PageSize>(StringComparer.OrdinalIgnoreCase)
        {
            // Letter size (8.5" x 11") = 612 x 792 points (1 point = 1/72 inch)
            { "Invoice", new PageSize(612, 792) },
            { "Report", PageSize.A4 },
            { "Brochure", PageSize.A5 }
            // Add more mappings here
        };

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            // Determine the target page size
            PageSize targetSize;
            if (!pageSizeMap.TryGetValue(fileName, out targetSize) || targetSize == null)
            {
                targetSize = PageSize.A4; // default size
            }

            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Use PdfPageEditor (Facade) to change page size
            var editor = new PdfPageEditor();
            // Bind the source PDF
            editor.BindPdf(pdfPath);
            // Set the desired page size for all pages
            editor.PageSize = targetSize;
            // Apply the changes to the document
            editor.ApplyChanges();
            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"Processed '{pdfPath}' -> '{outputPath}' with page size {targetSize}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
