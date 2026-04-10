using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Added for AnnotationFlags

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = "InputPdfs";
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
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_withSessionId.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Create a FormEditor facade bound to the document
                    using (FormEditor formEditor = new FormEditor(doc))
                    {
                        // Add a hidden text field named "SessionId" on page 1.
                        // The rectangle is placed off‑page (0,0,0,0) because the field is hidden.
                        formEditor.AddField(FieldType.Text, "SessionId", 1, 0, 0, 0, 0);

                        // Mark the field as hidden using the correct AnnotationFlags enum
                        formEditor.SetFieldAppearance("SessionId", AnnotationFlags.Hidden);
                    }

                    // Fill the newly created field with a generated GUID value
                    using (Form form = new Form(doc))
                    {
                        string guidValue = Guid.NewGuid().ToString();
                        form.FillField("SessionId", guidValue);
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
