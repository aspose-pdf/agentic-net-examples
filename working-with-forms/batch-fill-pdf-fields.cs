using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace BatchFillPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a folder and sample PDFs with a form field
            string inputFolder = "input";
            Directory.CreateDirectory(inputFolder);
            for (int i = 1; i <= 3; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"sample{i}.pdf");
                CreateSamplePdf(samplePath);
            }

            // Values to fill in each PDF
            string fieldName = "Name";
            string fieldValue = "John Doe";

            // Process each PDF in the folder
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            foreach (string pdfFile in pdfFiles)
            {
                using (Document pdfDoc = new Document(pdfFile))
                {
                    TextBoxField textBox = pdfDoc.Form[fieldName] as TextBoxField;
                    if (textBox != null)
                    {
                        textBox.Value = fieldValue;
                    }
                    // Overwrite the original file with the filled values
                    pdfDoc.Save(pdfFile);
                }
            }
        }

        private static void CreateSamplePdf(string filePath)
        {
            using (Document doc = new Document())
            {
                // Add a single page (1‑based indexing)
                doc.Pages.Add();

                // Define a rectangle for the text box field
                Rectangle rect = new Rectangle(100, 600, 300, 650);
                TextBoxField field = new TextBoxField(doc, rect);
                field.PartialName = "Name";
                field.Value = "";

                // Add the field to the first page of the form
                doc.Form.Add(field, 1);

                // Save the PDF
                doc.Save(filePath);
            }
        }
    }
}
