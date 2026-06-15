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

        // Initialize FormEditor and bind the PDF file
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Use the Form facade to retrieve field names
            Form form = new Form(doc);
            string[] fieldNames = form.FieldNames;

            // Display each field name on the console
            foreach (string name in fieldNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}