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
        // Folder where modified PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_SelectAll.pdf");

            try
            {
                // Load the PDF document
                Document doc = new Document(inputPath);

                // Define the checkbox rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Rectangle rect = new Rectangle(100, 700, 120, 720);

                // Create a checkbox field on the first page (pages are 1‑based)
                CheckboxField checkBox = new CheckboxField(doc.Pages[1], rect)
                {
                    PartialName = "SelectAll",
                    // Optional: set the default state (checked/unchecked)
                    Checked = false
                };

                // Add the field to the document's form (page index is 1‑based)
                doc.Form.Add(checkBox, 1);

                // Save the updated PDF
                doc.Save(outputPath);

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
