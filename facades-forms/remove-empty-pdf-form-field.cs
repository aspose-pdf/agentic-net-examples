using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "TempField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with FormEditor (facade) for editing
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Use the Form facade to read the current value of the field
            using (Form form = new Form(inputPath))
            {
                string fieldValue = form.GetField(fieldName);

                // Remove the field only if it contains no user input (null or empty)
                if (string.IsNullOrEmpty(fieldValue))
                {
                    editor.RemoveField(fieldName);
                }
            }

            // Save the (potentially) modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}