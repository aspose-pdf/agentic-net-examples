using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchBookletCreator
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where final booklets will be saved
        const string outputFolder = @"C:\OutputBooklets";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourceFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourceFile);
                string resizedFile = Path.Combine(outputFolder, fileNameWithoutExt + "_A5.pdf");
                string bookletFile = Path.Combine(outputFolder, fileNameWithoutExt + "_booklet.pdf");

                // ---------- Resize to A5 ----------
                // PdfPageEditor allows changing the page size of an existing PDF.
                var pageEditor = new PdfPageEditor();
                pageEditor.BindPdf(sourceFile);
                pageEditor.PageSize = PageSize.A5;          // Set target page size
                pageEditor.ApplyChanges();                  // Apply the size change
                pageEditor.Save(resizedFile);               // Save the resized PDF
                pageEditor.Close();                         // Release resources

                // ---------- Create booklet ----------
                // PdfFileEditor can generate a booklet; specify the same A5 size for the output.
                var fileEditor = new PdfFileEditor();
                bool success = fileEditor.MakeBooklet(resizedFile, bookletFile, PageSize.A5);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to create booklet for: {sourceFile}");
                }

                // Optional: delete the intermediate resized file
                if (File.Exists(resizedFile))
                {
                    File.Delete(resizedFile);
                }

                Console.WriteLine($"Processed '{sourceFile}' -> '{bookletFile}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourceFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch operation completed.");
    }
}