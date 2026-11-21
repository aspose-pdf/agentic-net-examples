using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

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

        // Load the PDF document containing form fields
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the source field whose value determines visibility
            // Replace "SourceField" with the actual field name
            TextBoxField sourceField = doc.Form["SourceField"] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine("Source field not found or not a TextBoxField.");
                return;
            }

            // Retrieve the target field whose visibility will be changed
            // Replace "TargetField" with the actual field name
            Field targetField = doc.Form["TargetField"] as Field;
            if (targetField == null)
            {
                Console.Error.WriteLine("Target field not found.");
                return;
            }

            // Example logic: make the target field visible only when the source field contains "Show"
            string sourceValue = sourceField.Value?.ToString() ?? string.Empty;
            bool shouldShow = sourceValue.Equals("Show", StringComparison.OrdinalIgnoreCase);

            // Set visibility using the Flags property (AnnotationFlags)
            // Hidden flag makes the field invisible; removing it makes the field visible.
            if (shouldShow)
            {
                // Ensure the field is not hidden. Keep other flags (e.g., Print) if needed.
                targetField.Flags &= ~AnnotationFlags.Hidden;
                // Optionally ensure the field is printable
                targetField.Flags |= AnnotationFlags.Print;
            }
            else
            {
                // Hide the field
                targetField.Flags |= AnnotationFlags.Hidden;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
