using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\Pdf\Input";
        // Folder where processed PDFs will be written
        const string outputFolder = @"C:\Pdf\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Example: delete page number 2 from each PDF
        const int pageToDelete = 2;

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Process files in parallel to improve throughput
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            string outputPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_modified.pdf");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // If the requested page exists, delete it; otherwise just copy the file
                if (pageToDelete >= 1 && pageToDelete <= doc.Pages.Count)
                {
                    // Delete the page and save the modified document
                    doc.Pages.Delete(pageToDelete);
                    doc.Save(outputPath);
                }
                else
                {
                    // No deletion needed – copy original PDF to the output location
                    File.Copy(pdfPath, outputPath, true);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputPath)}");
        });

        Console.WriteLine("All files have been processed.");
    }
}
