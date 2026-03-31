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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            FormEditor editor = new FormEditor(inputPath, outputPath);
            string javaScript = "event.target.value='';";
            // Attach JavaScript that clears the field when it receives focus
            editor.SetFieldScript("DiscountCode", javaScript);
            editor.Save();
            Console.WriteLine("JavaScript attached and saved to '" + outputPath + "'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}