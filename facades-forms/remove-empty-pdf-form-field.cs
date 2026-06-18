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

        // Retrieve the current value of the field
        string fieldValue;
        using (Form form = new Form(inputPath))
        {
            fieldValue = form.GetField(fieldName);
        }

        // If the field is empty (null or empty string), remove it
        if (string.IsNullOrEmpty(fieldValue))
        {
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);               // load the PDF
                editor.RemoveField(fieldName);           // delete the empty field
                editor.Save(outputPath);                 // save the modified PDF
            }
            Console.WriteLine($"Field \"{fieldName}\" was empty and has been removed.");
        }
        else
        {
            // Field contains data; copy the original PDF unchanged
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine($"Field \"{fieldName}\" contains data; PDF left unchanged.");
        }
    }
}