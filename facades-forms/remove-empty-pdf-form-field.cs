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

        // Read the current value of the field using the Form facade
        string fieldValue;
        using (Form form = new Form(inputPath))
        {
            // GetField returns null if the field does not exist
            fieldValue = form.GetField(fieldName);
        }

        // Remove the field only when it is empty or missing
        if (string.IsNullOrEmpty(fieldValue))
        {
            using (FormEditor editor = new FormEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Remove the specified field
                editor.RemoveField(fieldName);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Field \"{fieldName}\" removed. Output saved to \"{outputPath}\".");
        }
        else
        {
            Console.WriteLine($"Field \"{fieldName}\" contains data. No removal performed.");
        }
    }
}