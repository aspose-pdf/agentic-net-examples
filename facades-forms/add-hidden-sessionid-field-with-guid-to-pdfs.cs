using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAddHiddenSessionId
{
    static void Main()
    {
        // Base directory of the running application (works on Windows, Linux, macOS)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the folders exist (creates them if they are missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Add a hidden text field named "SessionId" on the first page.
                //    The field rectangle is set to zero size (0,0,0,0) to keep it
                //    invisible, and the AnnotationFlags.Hidden flag is applied.
                // -----------------------------------------------------------------
                using (FormEditor editor = new FormEditor(doc))
                {
                    // Add a text field with zero‑size rectangle on page 1
                    editor.AddField(FieldType.Text, "SessionId", 1, 0, 0, 0, 0);

                    // Mark the field as hidden
                    editor.SetFieldAppearance("SessionId", AnnotationFlags.Hidden);
                }

                // -----------------------------------------------------------------
                // 2. Fill the newly created field with a generated GUID value.
                // -----------------------------------------------------------------
                using (Form form = new Form(doc))
                {
                    string guidValue = Guid.NewGuid().ToString();
                    form.FillField("SessionId", guidValue);
                }

                // -----------------------------------------------------------------
                // 3. Save the modified document to the output folder.
                // -----------------------------------------------------------------
                doc.Save(outputPath);
                Console.WriteLine($"Processed: {fileName}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
