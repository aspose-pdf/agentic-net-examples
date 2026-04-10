using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "Header";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor and bind the source PDF
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPdf);

        // Configure visual appearance for the target field
        editor.Facade = new FormFieldFacade
        {
            // Example background color; replace with desired color or image handling
            BackgroundColor = System.Drawing.Color.LightGray,
            // Center the text within the field
            Alignment = FormFieldFacade.AlignCenter
        };

        // Apply the decoration to the specific field named "Header"
        editor.DecorateField(fieldName);

        // Save the modified document
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Field '{fieldName}' decorated and saved to '{outputPdf}'.");
    }
}