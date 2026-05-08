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
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Create a FormEditor bound to the loaded document
                    using (FormEditor editor = new FormEditor(doc))
                    {
                        // Generate a new GUID for this document
                        string guidValue = Guid.NewGuid().ToString();

                        // Add a hidden text field named "ProcessedDate"
                        // Coordinates are set to a zero‑size rectangle (invisible)
                        // Page number is 1 (Aspose.Pdf uses 1‑based indexing)
                        editor.AddField(FieldType.Text, "ProcessedDate", guidValue, 1, 0, 0, 0, 0);

                        // Mark the field as hidden so it does not appear in the UI
                        editor.SetFieldAppearance("ProcessedDate", AnnotationFlags.Hidden);

                        // Save changes back to the original file
                        editor.Save(pdfPath);
                    }
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