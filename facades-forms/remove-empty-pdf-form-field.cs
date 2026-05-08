using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "TempField";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF with FormEditor (facade for form manipulation)
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPdf);

            // Access the underlying Document to read field values
            Form form = new Form(editor.Document);

            // Retrieve the current value of the target field
            string fieldValue = form.GetField(fieldName);

            // If the field is empty (no user input), remove it
            if (string.IsNullOrEmpty(fieldValue))
            {
                editor.RemoveField(fieldName);
                Console.WriteLine($"Field '{fieldName}' was empty and has been removed.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' contains data and will be kept.");
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}