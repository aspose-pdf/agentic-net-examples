using System;
using System.IO;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonFieldName = "calcButton";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        string javaScript = "var price = this.getField('price').value;" +
                            "var qty = this.getField('quantity').value;" +
                            "var total = price * qty;" +
                            "this.getField('total').value = total;";
        formEditor.SetFieldScript(buttonFieldName, javaScript);
        // The FormEditor writes the modified PDF to outputPath automatically.
    }
}