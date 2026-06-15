using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Open the PDF document
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
                {
                    // Create a FormEditor bound to the document
                    FormEditor formEditor = new FormEditor(doc);

                    // Define field name and generate a GUID value
                    const string fieldName = "ProcessedDate";
                    string guidValue = Guid.NewGuid().ToString();

                    // Add a hidden text field on the first page (coordinates can be adjusted as needed)
                    // Parameters: FieldType, field name, page number (1‑based), llx, lly, urx, ury
                    formEditor.AddField(FieldType.Text, fieldName, 1, 50, 50, 200, 70);

                    // Fill the field with the generated GUID
                    Form formFacade = new Form(doc);
                    formFacade.FillField(fieldName, guidValue);

                    // Mark the field as hidden using AnnotationFlags
                    formEditor.SetFieldAppearance(fieldName, AnnotationFlags.Hidden);

                    // Save the modified PDF (overwrites the original file)
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