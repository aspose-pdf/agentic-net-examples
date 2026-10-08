using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: UpdateCreatorTool <folderPath> <creatorToolValue>");
            return;
        }

        string folderPath = args[0];
        string creatorTool = args[1];

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document, update its Creator metadata, and save back.
                using (Document pdfDoc = new Document(pdfPath))
                {
                    pdfDoc.Info.Creator = creatorTool; // Correct property for Creator metadata
                    pdfDoc.Save(pdfPath);               // Overwrite the original file
                }
                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to update {pdfPath}: {ex.Message}");
            }
        }
    }
}
