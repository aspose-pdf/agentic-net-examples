using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string pdfFolder = @"C:\PdfFolder";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Iterate over all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                Document pdfDoc = new Document(pdfPath);

                // Remove all annotations from every page
                foreach (Page page in pdfDoc.Pages)
                {
                    // Annotations collection can be null for some pages; guard against it
                    page.Annotations?.Clear();
                }

                // Save to a temporary file first to avoid corrupting the original if something fails
                string tempPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath)!,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_tmp.pdf");

                pdfDoc.Save(tempPath);

                // Replace the original file with the cleaned version
                File.Delete(pdfPath);
                File.Move(tempPath, pdfPath);

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All PDFs processed.");
    }
}
