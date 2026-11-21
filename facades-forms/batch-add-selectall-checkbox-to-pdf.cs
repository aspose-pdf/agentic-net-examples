using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be written
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_SelectAll.pdf");

            try
            {
                // Load the PDF document (Document implements IDisposable)
                using (Document doc = new Document(pdfPath))
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    Page firstPage = doc.Pages[1];

                    // Define the rectangle for the checkbox (lower‑left X/Y, upper‑right X/Y)
                    Rectangle checkboxRect = new Rectangle(100, 700, 120, 720);

                    // Create the checkbox field on the first page
                    CheckboxField selectAllCheckbox = new CheckboxField(firstPage, checkboxRect)
                    {
                        PartialName = "SelectAll",
                        Checked = false // optional default state
                    };

                    // Add the checkbox to the document's form collection
                    doc.Form.Add(selectAllCheckbox);

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
