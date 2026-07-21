using System;
using System.Drawing;                     // System.Drawing.Color is required for FormFieldFacade
using Aspose.Pdf.Facades;                // FormEditor and FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a FormEditor for the source PDF and specify the output file
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Set visual attributes via the Facade object
        formEditor.Facade = new FormFieldFacade();
        formEditor.Facade.BackgroundColor = Color.LightGreen;   // light green background

        // Apply the appearance changes to the field named "Status"
        formEditor.DecorateField("Status");

        // Persist the changes
        formEditor.Save();

        Console.WriteLine($"Field \"Status\" background set to light green and saved to '{outputPdf}'.");
    }
}