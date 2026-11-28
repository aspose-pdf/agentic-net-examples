using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source template and the output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "modified.pdf";

        // Name of the ListBox form field to modify
        const string listBoxFieldName = "ListBox1";

        // Verify that the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // FormEditor is a Facade class; it does NOT implement IDisposable,
        // so we do NOT wrap it in a using block.
        FormEditor formEditor = new FormEditor();

        // Bind the PDF document to the FormEditor instance
        formEditor.BindPdf(templatePath);

        // Add a new list item "Option A" to the specified ListBox field
        formEditor.AddListItem(listBoxFieldName, "Option A");

        // Save the modified PDF to the output path
        formEditor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}