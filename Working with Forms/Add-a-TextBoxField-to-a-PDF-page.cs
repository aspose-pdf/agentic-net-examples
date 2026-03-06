using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_textbox.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its form fields using FormEditor (Facades API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Define the rectangle for the text box (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            float llx = 100f; // left
            float lly = 500f; // bottom
            float urx = 300f; // right
            float ury = 530f; // top

            // Add a TextBox field named "MyTextBox" on page 1
            bool success = formEditor.AddField(FieldType.Text, "MyTextBox", 1, llx, lly, urx, ury);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the TextBox field.");
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with TextBox field saved to '{outputPath}'.");
    }
}