using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

namespace AsposePdfBatchImport
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create sample PDF files with a single text box field each.
            for (int i = 1; i <= 3; i++)
            {
                string pdfPath = $"sample{i}.pdf";
                using (Document doc = new Document())
                {
                    // Add a blank page (1‑based indexing).
                    doc.Pages.Add();
                    // Define the rectangle for the text box field.
                    Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
                    // Create the text box field on the first page.
                    TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect);
                    textField.PartialName = $"Field{i}";
                    // Add the field to the document's form collection.
                    doc.Form.Add(textField);
                    // Save the sample PDF.
                    doc.Save(pdfPath);
                }

                // Step 2: Create a matching JSON file containing the field value.
                string jsonPath = $"data{i}.json";
                string jsonContent = $"{{ \"Field{i}\": \"Value{i}\" }}";
                File.WriteAllText(jsonPath, jsonContent);
            }

            // Prepare lists of PDF and JSON file paths.
            List<string> pdfFiles = new List<string>();
            List<string> jsonFiles = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
                pdfFiles.Add($"sample{i}.pdf");
                jsonFiles.Add($"data{i}.json");
            }

            // Step 3: Import JSON data into each PDF concurrently.
            Parallel.For(0, pdfFiles.Count, index =>
            {
                string sourcePdf = pdfFiles[index];
                string sourceJson = jsonFiles[index];
                string outputPdf = $"filled{index + 1}.pdf";

                using (Document doc = new Document(sourcePdf))
                {
                    using (FileStream jsonStream = new FileStream(sourceJson, FileMode.Open, FileAccess.Read))
                    {
                        // Import form field values from the JSON stream.
                        doc.Form.ImportFromJson(jsonStream);
                    }
                    // Save the PDF with imported data.
                    doc.Save(outputPdf);
                }
            });

            Console.WriteLine("Batch import completed. Check the generated filled*.pdf files.");
        }
    }
}
