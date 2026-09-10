using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Added for correct FieldType enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button named "ResetForm" on page 1
                // Rectangle coordinates: llx, lly, urx, ury (example values)
                // Use the correct enum value for a push button (PushButton)
                bool buttonAdded = formEditor.AddField(FieldType.PushButton, "ResetForm", 1, 50, 750, 150, 800);
                if (buttonAdded)
                {
                    // Attach JavaScript to clear all form fields when the button is clicked
                    string resetScript = "this.resetForm();";
                    formEditor.AddFieldScript("ResetForm", resetScript);
                }
                // No explicit Save() needed; changes are applied to the Document instance
            }

            // Save the modified PDF to a new file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}
