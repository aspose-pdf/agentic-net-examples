using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchAddHiddenField
{
    static void Main()
    {
        // Folder containing the PDF forms
        const string inputFolder = "InputForms";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // ------------------------------------------------------------
                // 1. Add a hidden text field named "ProcessedDate" to the PDF.
                // ------------------------------------------------------------
                // Load the document.
                using (Document doc = new Document(pdfPath))
                {
                    // Generate a GUID that will be stored in the hidden field.
                    string guidValue = Guid.NewGuid().ToString();

                    // Create a zero‑size rectangle – the field will be invisible.
                    Rectangle rect = new Rectangle(0, 0, 0, 0);

                    // Create the hidden text field.
                    TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
                    {
                        PartialName = "ProcessedDate",
                        Value = guidValue
                    };

                    // Add the field to the document's form collection.
                    doc.Form.Add(hiddenField, 1);

                    // Save the changes back to the original file.
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
