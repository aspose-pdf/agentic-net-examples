using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class Program
{
    static void Main()
    {
        // Path to the XFDF file that contains the reviewer comments
        const string xfdfPath = "comments.xfdf";

        // Directory that holds the PDF files to which the comments will be applied
        const string pdfDirectory = "PdfFiles";

        // Verify that the XFDF file exists
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Verify that the PDF directory exists
        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"PDF directory not found: {pdfDirectory}");
            return;
        }

        // Get all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF document using Document (not PdfFileEditor)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Import the annotations from the XFDF file into the current PDF
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the modified PDF (overwrites the original file)
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Imported annotations into: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Annotation import completed for all PDFs.");
    }
}