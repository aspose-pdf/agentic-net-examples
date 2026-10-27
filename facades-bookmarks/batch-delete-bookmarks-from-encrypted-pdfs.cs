using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "ProcessedPdfs";
        // Password required to open the encrypted PDFs (user password)
        const string password = "userPassword";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the encrypted PDF providing the user password
                Document pdfDoc = new Document(pdfPath, password);

                // Remove all bookmarks (outlines) from the document
                // The Outlines collection represents the PDF bookmarks.
                pdfDoc.Outlines.Clear();

                // Save the modified PDF to the output folder
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (InvalidPasswordException ex)
            {
                Console.Error.WriteLine($"Invalid password for {fileName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
