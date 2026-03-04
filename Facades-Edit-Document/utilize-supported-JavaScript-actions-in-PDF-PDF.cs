using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PDF content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF for editing
        editor.BindPdf(inputPath);

        // Add a JavaScript action that runs when the document is opened
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
            "app.alert('Welcome to the PDF!');");

        // Add a JavaScript action that runs when the document is closed
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentClose,
            "app.alert('Good-bye!');");

        // Save the modified PDF with the embedded JavaScript actions
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with JavaScript actions to '{outputPath}'.");
    }
}