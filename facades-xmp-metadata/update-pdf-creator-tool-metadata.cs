using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect the folder path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: UpdateCreatorTool <folderPath>");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Value to set for the Creator metadata property
        const string newCreatorTool = "MyUtility v1.0";

        // Retrieve all PDF files in the specified folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF. If the file is encrypted, Aspose.Pdf throws PdfException.
                using (Document doc = new Document(pdfPath))
                {
                    // Update the Creator metadata field via DocumentInfo
                    doc.Info.Creator = newCreatorTool;
                    // Overwrite the original file
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (PdfException ex)
            {
                // Skip encrypted PDFs (or any other load‑time issues specific to encryption)
                Console.Error.WriteLine($"Skipped encrypted or unreadable file {pdfPath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to update {pdfPath}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing complete.");
    }
}
