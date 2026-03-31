using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "input-pdfs";
        // Folder where modified PDFs will be saved
        string outputFolder = "output-pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_with_checkbox.pdf");

            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count < 1)
                {
                    Console.Error.WriteLine($"No pages in {inputPath}");
                    continue;
                }

                Page firstPage = doc.Pages[1];
                // Define rectangle for the checkbox (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
                CheckboxField checkbox = new CheckboxField(firstPage, rect);
                checkbox.Name = "SelectAll";
                checkbox.ExportValue = "On";
                // Add the checkbox to the document's form collection
                doc.Form.Add(checkbox);
                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Processed {fileName} -> {outputPath}");
            }
        }
    }
}
