using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag

class Program
{
    static void Main()
    {
        const string inputPath = "feedback_form.pdf";
        const string outputPath = "required_feedback.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create FormEditor and bind the existing PDF.
        using (var formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Add a required text field named "Feedback" on page 1.
            // Parameters: field type, name, page (1‑based), llx, lly, urx, ury.
            formEditor.AddField(FieldType.Text, "Feedback", 1, 100, 600, 300, 630);

            // Mark the field as required.
            formEditor.SetFieldAttribute("Feedback", PropertyFlag.Required);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved required feedback form to '{outputPath}'.");
    }
}
