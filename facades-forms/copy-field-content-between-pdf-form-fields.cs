using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing the fields
        const string outputPdf = "output.pdf";         // Result PDF with copied content

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Form facade works with AcroForm fields. It provides GetField and FillField methods.
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Retrieve the current value of the source field.
            string sourceValue = form.GetField("SourceNotes");

            // If the source field is empty, nothing to copy.
            if (sourceValue == null)
            {
                Console.WriteLine("Source field 'SourceNotes' not found or empty.");
            }
            else
            {
                // Fill the target field with the retrieved value.
                bool filled = form.FillField("TargetNotes", sourceValue);
                Console.WriteLine(filled
                    ? "Content copied from 'SourceNotes' to 'TargetNotes'."
                    : "Target field 'TargetNotes' not found.");
            }

            // Save the modified document.
            form.Save();
        }

        Console.WriteLine($"Processed PDF saved as '{outputPdf}'.");
    }
}