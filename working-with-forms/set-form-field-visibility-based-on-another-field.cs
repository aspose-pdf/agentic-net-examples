using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Source field whose value determines visibility (e.g., a checkbox named "ShowDetails")
            CheckboxField sourceField = doc.Form["ShowDetails"] as CheckboxField;

            // Target field whose visibility will be changed (e.g., a text box named "Details")
            TextBoxField targetField = doc.Form["Details"] as TextBoxField;

            if (sourceField != null && targetField != null)
            {
                // If the checkbox is checked, make the target field visible; otherwise hide it
                bool isChecked = sourceField.Checked;

                // Form fields are widget annotations; they inherit the Flags property from Annotation.
                // Use AnnotationFlags.Hidden to control visibility.
                if (isChecked)
                {
                    // Remove the Hidden flag to show the field
                    targetField.Flags &= ~AnnotationFlags.Hidden;
                }
                else
                {
                    // Add the Hidden flag to hide the field
                    targetField.Flags |= AnnotationFlags.Hidden;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
