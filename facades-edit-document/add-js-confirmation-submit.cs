using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitButtonName = "SubmitBtn";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string jsCode = "if(app.alert('Do you want to submit the form?', 3) == 4) { this.submitForm(); }";

        try
        {
            FormEditor editor = new FormEditor();
            editor.BindPdf(inputPath);
            editor.SetFieldScript(submitButtonName, jsCode);
            editor.Save(outputPath);
            Console.WriteLine($"JavaScript confirmation added to button '{submitButtonName}' and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
