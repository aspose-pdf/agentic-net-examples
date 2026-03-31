using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string javaScript = "if (event.value < 18) { app.alert('Age must be at least 18'); }";

        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);
            bool added = formEditor.AddFieldScript("Age", javaScript);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add JavaScript to the field 'Age'.");
            }
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached to field 'Age' and saved to '{outputPath}'.");
    }
}