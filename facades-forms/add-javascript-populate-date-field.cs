using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // JavaScript that runs when the document is opened.
        // It retrieves the field named "Date" and sets its value to the current date.
        string jsCode = "var f = this.getField('Date'); if (f) { f.value = (new Date()).toLocaleDateString(); }";

        // Attach the JavaScript to the document Open event
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}