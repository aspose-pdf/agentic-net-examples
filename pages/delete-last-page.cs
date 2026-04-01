using System;
using System.IO;
using Aspose.Pdf;

public class DeleteLastPageExample
{
    public static void Main()
    {
        // Folder containing PDF files to process
        string inputFolder = "InputPdfs";
        // Folder where the modified PDFs will be saved
        string outputFolder = "OutputPdfs";

        // Ensure the input directory exists – if it does not, create it and inform the user.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now.");
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine("Place PDF files into the input folder and re‑run the program.");
            return; // Exit – there is nothing to process yet.
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Delete the last page if the document has at least one page (pages are 1‑based)
                if (pdfDocument.Pages.Count > 0)
                {
                    pdfDocument.Pages.Delete(pdfDocument.Pages.Count);
                }

                // Build the output file path (preserve original file name)
                string fileName = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the modified document
                pdfDocument.Save(outputPath);
            }
        }

        Console.WriteLine("Last pages removed from all PDFs.");
    }
}
