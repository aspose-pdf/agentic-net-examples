using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind a FormEditor to the document for form manipulation
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button named "ResetForm" on page 1
                // Coordinates are supplied as floats (left, bottom, right, top)
                formEditor.AddField(
                    FieldType.PushButton,
                    "ResetForm",
                    1,
                    100f,
                    500f,
                    200f,
                    540f);

                // Attach JavaScript that clears all form fields when the button is clicked
                // The PDF JavaScript method resetForm() resets the entire form
                formEditor.AddFieldScript("ResetForm", "this.resetForm();");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}
