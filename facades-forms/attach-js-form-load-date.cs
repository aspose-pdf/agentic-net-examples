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

        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        string javaScript = "this.getField('Date').value = util.printd('dd/MM/yyyy', new Date());";

        bool success = formEditor.SetFieldScript("Date", javaScript);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set JavaScript for the field.");
        }

        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}