using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the content editor and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // JavaScript that defines a translation dictionary and a function to apply a label
        string jsCode = @"
var translations = {
    'en': { 'label': 'Hello' },
    'fr': { 'label': 'Bonjour' },
    'es': { 'label': 'Hola' }
};

function setLocale(locale) {
    var dict = translations[locale];
    if (dict) {
        // Assume a form field named 'myLabel' exists in the PDF
        var f = this.getField('myLabel');
        if (f) {
            f.value = dict.label;
        }
    }
}
";

        // Attach the JavaScript to the document open event
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF with translation JavaScript saved to '{outputPath}'.");
    }
}