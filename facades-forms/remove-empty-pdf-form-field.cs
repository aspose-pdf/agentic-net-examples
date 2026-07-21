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
        Form form = new Form(inputPath);
        string fieldValue = form.GetField(fieldName);
        form.Close(); // release resources

        // If the field is empty (no user input), remove it with FormEditor
        if (string.IsNullOrEmpty(fieldValue))
        {
            // FormEditor is initialized with source and destination files
            FormEditor editor = new FormEditor(inputPath, outputPath);
            editor.RemoveField(fieldName); // delete the empty field
            editor.Save();                 // persist changes to outputPath
            editor.Close();                // clean up

            Console.WriteLine($"Field '{fieldName}' was empty and has been removed. Saved to '{outputPath}'.");
        }
        else
        {
            // Field contains data; simply copy the original PDF to the output location
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine($"Field '{fieldName}' contains data; no removal performed.");
        }
    }
}