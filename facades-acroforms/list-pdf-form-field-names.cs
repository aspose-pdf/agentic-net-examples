using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Initialize FormEditor with the loaded document (as required)
            using (FormEditor editor = new FormEditor(doc))
            {
                // Use the Form facade to get the list of field names
                Form form = new Form(doc);
                string[] fieldNames = form.FieldNames;

                Console.WriteLine("Form field names:");
                foreach (string name in fieldNames)
                {
                    Console.WriteLine(name);
                }

                // No changes made; editor will be disposed automatically
            }
        }
    }
}