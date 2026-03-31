using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string resetButtonName = "resetBtn";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        string javaScript = "var i; for (i=0;i<this.numFields;i++) { var f=this.getField(this.getFieldName(i)); if (f.display==display.hidden) f.value=''; } this.resetForm();";

        formEditor.SetFieldScript(resetButtonName, javaScript);
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine("JavaScript attached and saved to '" + outputPath + "'.");
    }
}