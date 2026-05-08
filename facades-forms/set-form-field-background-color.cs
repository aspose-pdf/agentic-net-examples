using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a FormEditor, bind the PDF, set the background color for the "Status" field,
        // apply the changes, and save the result.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Configure visual appearance via FormFieldFacade
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightGreen; // light green background

            // Apply the facade settings to the specific field named "Status"
            editor.DecorateField("Status");

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"Status\" background set to light green. Saved to '{outputPdf}'.");
    }
}