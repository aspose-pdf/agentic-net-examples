using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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
                Document doc = new Document(pdfPath);

                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                    doc.Pages.Add();

                // Create a hidden text field named "ProcessedDate" on the first page
                // Use a zero‑size rectangle so the field is invisible
                Page page = doc.Pages[1];
                var hiddenField = new TextBoxField(page, new Rectangle(0, 0, 0, 0))
                {
                    PartialName = "ProcessedDate",
                    Flags = AnnotationFlags.Hidden // hide the field
                };

                // Add the field to the form (page index is 1‑based)
                doc.Form.Add(hiddenField, 1);

                // Fill the hidden field with a generated GUID
                hiddenField.Value = Guid.NewGuid().ToString();

                // Save the modified PDF back to the same path
                doc.Save(pdfPath);

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
