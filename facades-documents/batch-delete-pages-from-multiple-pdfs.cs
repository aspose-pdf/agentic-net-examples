using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        string folderPath = "PDFs";

        // Pages to delete (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToDelete = new int[] { 1, 2 };

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Loop through all PDF files in the directory
        foreach (string inputPath in Directory.GetFiles(folderPath, "*.pdf"))
        {
            // Create an output file name (e.g., original_trimmed.pdf)
            string outputPath = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputPath) + "_trimmed.pdf");

            try
            {
                // Load the PDF document
                Document pdfDoc = new Document(inputPath);

                // Delete the specified pages. Iterate in descending order to keep indexes stable.
                Array.Sort(pagesToDelete); // ensure ascending order first
                for (int i = pagesToDelete.Length - 1; i >= 0; i--)
                {
                    int pageNumber = pagesToDelete[i];
                    if (pageNumber > 0 && pageNumber <= pdfDoc.Pages.Count)
                    {
                        pdfDoc.Pages.Delete(pageNumber);
                    }
                }

                // Save the modified document
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
