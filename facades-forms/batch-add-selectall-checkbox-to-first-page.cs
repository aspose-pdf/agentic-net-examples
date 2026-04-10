using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(sourcePath))
                {
                    // Bind the document to FormEditor (facade API)
                    using (FormEditor formEditor = new FormEditor(doc))
                    {
                        // Add a checkbox field named "SelectAll" on the first page
                        // Coordinates: lower‑left (50,750), upper‑right (70,770)
                        bool success = formEditor.AddField(
                            FieldType.CheckBox,   // field type
                            "SelectAll",          // field name
                            1,                    // page number (1‑based)
                            50f, 750f, 70f, 770f // llx, lly, urx, ury
                        );

                        if (!success)
                        {
                            Console.Error.WriteLine($"Failed to add checkbox to {fileName}");
                        }

                        // Save the updated PDF (lifecycle rule: use Save)
                        formEditor.Save(targetPath);
                    }
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}