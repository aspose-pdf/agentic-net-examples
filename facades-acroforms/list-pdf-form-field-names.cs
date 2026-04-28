using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using FormEditor
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath); // initialize the facade with the PDF file

            // Access the underlying Document object
            Document doc = editor.Document;

            // Use the Form facade to retrieve field names
            Form form = new Form(doc);
            string[] fieldNames = form.FieldNames;

            Console.WriteLine("Form field names:");
            foreach (string name in fieldNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}