using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputFolder = "InputForms";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Generate a new GUID for this document
                    string guid = Guid.NewGuid().ToString();

                    // Create a hidden text field named "ProcessedDate" on page 1.
                    // The rectangle is zero‑size so the field is not visible.
                    var rect = new Rectangle(0, 0, 0, 0);
                    var txtField = new TextBoxField(doc.Pages[1], rect)
                    {
                        PartialName = "ProcessedDate",
                        Value = guid
                    };
                    // No need to set a FieldFlag; the zero‑size rectangle already hides the field.

                    // Add the field to the form on page 1.
                    doc.Form.Add(txtField, 1);

                    // Overwrite the original PDF with the updated version.
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
