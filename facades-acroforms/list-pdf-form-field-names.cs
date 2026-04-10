using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Form class resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf"; // required by FormEditor constructor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor loads the PDF and prepares an output file.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Access the underlying Document via the editor.
            // Use the Form facade to obtain field names.
            using (Form form = new Form(editor.Document))
            {
                string[] fieldNames = form.FieldNames;
                Console.WriteLine("Form field names:");
                foreach (string name in fieldNames)
                {
                    Console.WriteLine(name);
                }
            }

            // No changes made, but Save() fulfills the required lifecycle.
            editor.Save();
        }
    }
}