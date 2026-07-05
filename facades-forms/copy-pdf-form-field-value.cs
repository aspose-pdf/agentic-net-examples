using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing both fields
        const string outputPdf = "output.pdf";  // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Form facade works with an input and an output file.
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
                        ? "Value copied to 'TargetNotes'."
                        : "Target field 'TargetNotes' not found.");
                }

                // Persist changes to the output PDF.
                form.Save();
            }

            Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}