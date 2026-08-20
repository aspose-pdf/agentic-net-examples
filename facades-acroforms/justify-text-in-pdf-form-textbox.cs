using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TextBox1"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with input and output PDF files
        Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor(inputPath, outputPath);

        // Configure the facade to use justified alignment
        Aspose.Pdf.Facades.FormFieldFacade facade = new Aspose.Pdf.Facades.FormFieldFacade
        {
            Alignment = Aspose.Pdf.Facades.FormFieldFacade.AlignJustified
        };
        formEditor.Facade = facade;

        // Apply the visual changes to the specified textbox field
        formEditor.DecorateField(fieldName);

        // Persist the changes to the output PDF
        formEditor.Save();

        Console.WriteLine($"Justified text field saved to '{outputPath}'.");
    }
}