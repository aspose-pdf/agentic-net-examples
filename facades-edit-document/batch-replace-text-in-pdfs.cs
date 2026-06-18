using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where the modified PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(outputFolder, fileName);

            // PdfContentEditor implements IDisposable, so use a using block
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF file
                editor.BindPdf(sourcePath);

                // Replace all occurrences of "Confidential" with "Public" on all pages (page = 0)
                editor.ReplaceText("Confidential", 0, "Public");

                // Save the edited PDF to the destination path
                editor.Save(destinationPath);
            }

            Console.WriteLine($"Replaced text in: {fileName}");
        }
    }
}