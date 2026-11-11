using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TempField";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Read the current value of the field using the Form facade
        string fieldValue;
        using (Form form = new Form())
        {
            form.BindPdf(inputPath);
            // GetField returns the field's value as a string (empty string if the field has no data)
            fieldValue = form.GetField(fieldName);
        }

        // Delete the field only when it contains no user‑entered data
        if (string.IsNullOrEmpty(fieldValue))
        {
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);
                // FormEditor does not have DeleteField; the correct method is RemoveField
                editor.RemoveField(fieldName);
                editor.Save(outputPath);
            }
        }
        else
        {
            // No deletion required – just copy the original PDF to the output location
            File.Copy(inputPath, outputPath, true);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
