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
        Document doc = new Document(pdfPath);

        // Initialize FormEditor using the BindPdf overload (no string ctor in this version)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(doc);
            // No modifications are made, so we just let the using block dispose it.
        }

        // Retrieve all form field names
        Form form = new Form(doc);
        string[] fieldNames = form.FieldNames;

        // Display each field name on the console
        foreach (string name in fieldNames)
        {
            Console.WriteLine(name);
        }
    }
}
