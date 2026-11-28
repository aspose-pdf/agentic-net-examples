using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "multiScript.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Individual JavaScript statements.
        string openScript = "app.alert('Welcome to the PDF!');";
        string printScript = "this.print({bUI:true,bSilent:false,bShrinkToFit:true});";

        // Combine them into a single script because Aspose.Pdf supports only one OpenAction.
        string combinedScript = $"{openScript} {printScript}";

        // Assign the combined script to the document's OpenAction.
        doc.OpenAction = new JavascriptAction(combinedScript);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"PDF with multiple JavaScript actions saved to '{outputPath}'.");
    }
}
