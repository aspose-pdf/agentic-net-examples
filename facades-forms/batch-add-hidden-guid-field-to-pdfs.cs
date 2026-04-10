using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFolder  = "InputForms";
        const string outputFolder = "OutputForms";

        // Ensure both input and output directories exist.
        // If the input folder does not exist we create it and exit – the user can drop PDFs there.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' was not found and has been created. Place PDF files there and re‑run the program.");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // ---------- Add hidden field ----------
                using (FormEditor editor = new FormEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(inputPath);

                    // Add a tiny text field on page 1 (coordinates are near‑zero so the field is invisible)
                    editor.AddField(FieldType.Text, "ProcessedDate", 1, 0f, 0f, 1f, 1f);

                    // Mark the field as read‑only – a read‑only field that is virtually zero‑size is hidden from the UI.
                    editor.SetFieldAttribute("ProcessedDate", PropertyFlag.ReadOnly);

                    // Save the intermediate PDF (with the new field) to the output location
                    editor.Save(outputPath);
                }

                // ---------- Fill the hidden field with a GUID ----------
                using (Form form = new Form(outputPath))
                {
                    string guidValue = Guid.NewGuid().ToString();
                    form.FillField("ProcessedDate", guidValue);
                    // Persist the change back to the same file
                    form.Save(outputPath);
                }

                Console.WriteLine($"Processed '{fileName}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
