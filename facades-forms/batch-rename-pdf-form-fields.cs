using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with source and destination PDFs
        Form form = new Form(inputPdf, outputPdf);

        // Loop through all form fields and rename those starting with "Old_"
        foreach (string fieldName in form.FieldNames)
        {
            if (fieldName.StartsWith("Old_"))
            {
                string newName = "New_" + fieldName.Substring("Old_".Length);
                form.RenameField(fieldName, newName);
            }
        }

        // Persist the changes to the output PDF
        form.Save();

        Console.WriteLine($"Fields renamed and saved to '{outputPdf}'.");
    }
}